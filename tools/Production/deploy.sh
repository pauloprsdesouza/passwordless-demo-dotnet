#!/bin/bash

set -e

# Determine the environment based on the current directory
envDir=$(basename $(cd $(dirname $0) ; pwd))
environment=$(echo $envDir | awk '{print toupper(substr($0,1,1))tolower(substr($0,2))}')

# Set the base directory relative to the script location
basedir=$(cd $(dirname $0) ; pwd)/../..

# Navigate to the source directory
cd $basedir/src

# Clean and create the distribution directory
rm -rf $basedir/dist
mkdir $basedir/dist

# Copy the CloudFormation template from the environment-specific folder
cp $basedir/tools/$environment/CloudFormation.yaml $basedir/dist

# Publish the .NET project
dotnet publish Schedy.sln -c release -o $basedir/dist

# Convert environment to lowercase for stack name
lowercaseEnvironment=$(echo $environment | awk '{print tolower($0)}')

# Package and deploy using AWS CloudFormation
aws cloudformation package \
  --template-file $basedir/dist/CloudFormation.yaml \
  --output-template-file $basedir/dist/deploy.yaml \
  --s3-bucket pauloprsdesouza-dev \
  --s3-prefix cloud-formation/schedy-hermes/$lowercaseEnvironment

aws cloudformation deploy \
  --stack-name schedy-hermes-$lowercaseEnvironment \
  --template-file $basedir/dist/deploy.yaml \
  --parameter-overrides Environment=$environment JwtSecret=87c10446-aa6a-4df3-8615-d4302cd205fb \
  --capabilities CAPABILITY_IAM
