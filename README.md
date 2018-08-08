# Frame

## Developers notes

### Frame UI

- Ensure [Node.js](https://nodejs.org/en/) is installed and set-up on your machine
- Clone the [frame repository](https://github.com/gpit-futures/frame.git) using git clone (or your favourite GUI tool) using the URL https://github.com/gpit-futures/frame.git to an appropriate local directory.
- Open your chosen console (Node.js Command Prompt/Git-Bash/etc.).
- Navigate to `<downloaded location>/frame/electron-frame` and run the following commands:
  - `npm install`
  - `npm install electron-forge`
  - `electron-forge start` - (To start the application locally.)
  - `electron-forge make` - (To compile an executable for your environment. After compilation, the executable can be found in the `/out/make` directory.)
- Precompiled executables for windows/mac can be found [here](https://github.com/gpit-futures/frame/releases)

### Frame Backend

- Ensure [.NET Core 2.1 SDK](https://www.microsoft.com/net/download) is installed.
- Visual Studio 2017

## Dependencies

- A working version of the [core module](https://github.com/gpit-futures/pulse) and any other modules:
  - [inr module](https://github.com/gpit-futures/inr)
  - [appointment module](https://github.com/gpit-futures/gpconnect-demonstrator)
- A working [authentication server](https://github.com/gpit-futures/auth-server)
- A working [rabbit server](https://github.com/rabbitmq/rabbitmq-server/releases)

## Configuration

- this.frameworkBackendUrl = "http://ec2-18-130-26-44.eu-west-2.compute.amazonaws.com:8080" - used to define where the back-end api is hosted.

## Installation notes

To install the framework directly as an executable file simply download the [latest version](https://github.com/gpit-futures/frame/releases) from Git Hub - note, that in order to show the communication between the systems, download the [thick-client](https://github.com/gpit-futures/thick-client/releases) as well.