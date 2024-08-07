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

This solution was developed using .NET 8.0.300. We have implemented a `classlib` and `webapi` project.

## Quick start

Wishful thinking.

## Usage

As this is a lower-level service, there is not much "quick start" opportunity. Proceed to set up [PyAcct](https://www.github.com/mwhicks-dev/pyacct/tree/v1.0), and after that set the following required environment variables:

* `PYACCT_URI`: Set this to the HTTP protocall (`http://` if you're not sure or `https://` if you are sure) that PyAcct is running on, followed by the address -- domain name or IP address it is hosted on. If you're running CSChatLogger on the same machine as PyAcct, this will be `localhost` or `127.0.0.1`.
* `PYACCT_PORT`: Set this to the port you selected to run PyAcct on. If you did not configure this during PyAcct setup, this will be `8000`.

Next, go to the `Api/` directory, make a copy of `appsettings.json.template` (with the `.template` removed), and set the connection string to one appropriate for your environment. This can be tricky, so don't get discouraged if you have to do some debugging.

After that, simply run the following, and accept any pop-ups:

```bash
dotnet dev-certs https --trust
dotnet run --project Api/ --urls "https://localhost:7225/"
```

Once completed, you can access the API GUI at [https://localhost:7225/swagger/index.html](https://localhost:7225/swagger/index.html), or use the API documentation linked to in the Introduction to make HTTP calls to the hosting address on port 7225.

You can, of course, also pick any other port. 7225 is pretty out of the way.

## Known issues and limitations

Message storage and data transfer are unencrypted.

I don't want to test it because I'm not getting paid to do that.

## Getting help

Feel free to open any issues if you end up needing some help. If need be, I'll make a bug reporting guide and FAQ, but if you don't see those, go ahead and wing it.

## Contributing

Also feel free to fork and extend this however you'd like to.

## License

As this software is strictly open-source, I will not be held liable for any losses suffered by individuals or enterprises at the result of its use. If you want to use this for something important, you should look through the source code carefully and identify any liabilities that could cause you problems later.

## Acknowledgements

* Official C#, .NET Core and ASP.NET docs, of course.
* [READMINE](https://mhucka.github.io/readmine/) for README formatting.
