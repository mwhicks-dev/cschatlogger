# cschatlogger

C#-based chat client that sits atop another project, [PyAcct](https://www.github.com/mwhicks-dev/pyacct).

## Table of contents

* Introduction
* Installation
* Known issues and limitations
* Acknowledgements

## Introduction

CSChatLogger is part of a larger project I am working on in order to practice service-oriented architecture/microservices and software architecture. The service allows for authenticated accounts to create one-on-one or group chats where they can send and recieve text-based messages (albeit unencrypted). This doesn't do anything revolutionary, but it is mine. As the name suggests, I am developing this component in C#.NET.

## Installation

This service was developed using .NET 8.0.300.

## Known issues and limitations

For now, messages and data transfer are unencrypted. Of course, they will be encrypted by your database of choice if you choose for them to be.

## Acknowledgements

* [READMINE](https://mhucka.github.io/readmine/) for README formatting.
