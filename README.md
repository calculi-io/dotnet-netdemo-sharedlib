# NetDemo Shared Library

This is a sample shared library intended for use with:

- .NET 4.7.2+
- Visual Studio Enterprise 2017

The following info is all you need to create a CI/CD pipeline for this library within Guide-Rails&#174;.

## Pipeline Definition

We've already created a pipeline definition for this library in `/ci/guide-rails.json`. Once onboarded to Guide-Rails&#174;, your pipeline will consist of the following segments:

- Build - compiles, unit tests and publishes the library to an artifact repository

Other components (netdemo-server and netdemo-client) will consume this library as part of their CI/CD process

## Onboarding This Repository

1. Fork this repository and add to your preferred source code management server (Bitbucket, Github, Gitlab, etc.) 
2. Log into Guide-Rails&#174; and head to the Configuration Console (aka Onboarding) and add a new Component
3. In the Component configuration view, choose your source code management server and enter your repository clone URL (https or SSH)
4. Verify the properties described in the rest of this document are set properly in your configuration

### Code Analysis

| Name | Value | Description |
| ---- | ----- | ----------- |
| Run Code Analysis | [&#x2713;] | |
| Source Directories | local-sonar-project.properties | Temporarily set this value, should not need to in the future. Proper usage is not to set this value. |
| Additional Properties Files | local-sonar-project.properties | sonar project properties file |

> **Note: Existing Quality Gates**
>
>If this pipeline fails to pass the build segment because of Quality Gates, you
can override them in the Component settings.

### Properties

Add the below properties in the Properties section. If desired, these properties can be set on a parent resource to avoid having to set the same values in different repositories.

| Name | Type | Value |
| ---- | ---- | ----- |
| consul.tag | string | ((application.shortsha)) | |
| consul.servicename | string | ((application.name)) | |

## Existing Quality Gates

If this pipeline fails to pass the build segment because of Quality Gates, you
can override them in the Component settings.

## NuGet Integration

The file SharedLib\package.ps1 will place the file in a local nuget repository
in the temp directory. To use your preferred NuGet repository, modify this file
as desired.
