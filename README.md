# cschatlogger

C#-based chat client that sits atop another project, [PyAcct](https://www.github.com/mwhicks-dev/pyacct).

## Table of contents

* Introduction
* Installation
* Known issues and limitations
* Acknowledgements

## Introduction

CSChatLogger is part of a larger project I am working on in order to practice service-oriented architecture/microservices and software architecture. The service allows for authenticated accounts to create one-on-one or group chats where they can send and recieve text-based messages (albeit unencrypted). This doesn't do anything revolutionary, but it is mine. As the name suggests, I am developing this component in C#.NET.

A description of all of the data structures used in this service can be found [here](https://github.com/mwhicks-dev/cschatlogger/wiki/Business-Rules). A less-rough description of the current REST API can be found [here](https://github.com/mwhicks-dev/cschatlogger/wiki/CSChatLogger-API-v1).

## Installation

This solution was developed using .NET 8.0.300. We have implemented a `classlib` and `xunit` project.

## Known issues and limitations

Message storage and data transfer are unencrypted.

## Acknowledgements

* Official C#, .NET Core and ASP.NET docs, of course.
* [READMINE](https://mhucka.github.io/readmine/) for README formatting.
