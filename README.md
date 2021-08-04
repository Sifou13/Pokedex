# Pokedex

**Intro**

This application was created and designed to showcase and create a discussion around Software Architecture, Design and Software Engineering principles, methodologies and also help in touching on team level (scaling engineers) and business level implications (i.e.Devops business requirements); 

This can therefore be used as a support to drive an interview where a hiring manager could review the work done on here, while asking further question around the specifics requirement built in the app.

To showcase distributed team organisation as well as software lifecycle, Gihub Issues were also created and pull requests mapped back to issues, so that it reflects what normally happen when developing in a comercial context. 

As this was a single man's initial project, there was no code review involved from peers, the best ally to mitigate this quality lack had to be the unit tests with sufficient code and use case coverage. 



**The Application**

The application is the first steps or an MVP (Minimum Viable Product) for a future potential Pokedex Data Provider, via REST APIs to retrieve the Pokemon's details.

Part of the high level Architecture Designs for this specific app are can be found at the following location: https://drive.google.com/drive/folders/1FPFtM8B25aD7ZjPY9T7WjztUS8TrCVm-

Those designs create a communication tool for Engineers, and Architects, but sometime also to convey a blocker where business requirements and a technical ones come into conflict. it's a way to allow non-technical stakeholders being involved in some of those conversations. Tose designs and the app architecture in general was approached using the IDesign Methodology (Juval Lowy, https://www.idesign.net/).

To develop this application we decided to use .Net Core (3.1) since it is the most recent and stable version of the .Net Core Framework, for which Microsoft will offer long term support. It was developped using Visual Studio 2019.

The Visual Studio solution comprises of three main projects (and Unit test projects):
- **The Pokedex.Api project**, which deals with the web requests and any web architecture specifics (Caching, calling the business layer, and later on, if and when required, security) - This should and has been kept thin - This project is of Type Asp.Net Core; it was started empty and each required piece of required middleware was added when it became a requirement only.

- **The Pokedex.Services**: a class library  This is the core of our application and will contain:
     - Business logic that can be mapped back to the initial Product Requirements.
     - Data Access - 

- **The Pokedex.Services.Contract**: This is the project that will be exposed to the external world and that will be used to generate client proxys:
   - It will surface the public Operations and Object consumers need to know about
   

The third party dependencies used by the different projects so far are as follow:
- AutoMapper - v10.1.1: https://www.nuget.org/packages/AutoMapper/10.1.1/ - (Both Api and Service Projects)
- Microsoft Test Framework (Unit Testing Projects)
- Moq (Unit Testing Projects)




**How to Run it?**

The below steps assume you have Docker installed on your local machine.

How To run this application on Windows:

- **Clone Repository to local**
- Navidate to the repository on your local - you need to be at the root of the repository folder, where the  solution file (Pokedex.sln) file can be found.
- Create a Docker image using the command below; note that this step will be using the DockerFile file found in this folder to create the immage:
  - **docker.exe build -t pokedex .**
  - if this steps fails because of soe networking issues, please refer to the "Possible Known Build Issues" --> "Networking - **Change Local Network Adapter Interface Metric**", this could be caused as this container will use the internet to retrieve data 
  - now, your machine should contain a few docker images which compose the "pokedex" docker image that we will use in the next step.
- Now, time to run an instance of our Docker image - run the following:
  - **docker run -it --rm -p 5000:80 --name pokedex-instance pokedex**
  - this creates an instance of our pokedex image called pokedex-instance and runs it, mapping its internal 80 port to the port 5000 on your local machine
  - now, you can start using the api endpoints; here are two examples you can start with
    - http://localhost:5000/api/pokemon/pikachu
    - http://localhost:5000/api/pokemon/translated/pikachu






**Possible Known Build Issues** 

**Networking - Change Local Network Adapter Interface Metric**

In Powershell, on the host: Get the interface detail for the network adapter you want to map your container to, generally called "ethernet" or Wifi"  
Get-NetIPInterface -AddressFamily IPv4 | Sort-Object -Property InterfaceMetric -Descending

if the interface metric of the adapter you want to use is not the lowest in the listed interfaces, Set it to be the one with the lowest interface metric; here we want to use the adapter called "WIFI".
Set-NetIPInterface -InterfaceAlias 'WIFI' -InterfaceMetric 1


