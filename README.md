#### Table of Contents
- 1 NationalLandmarks
  - 1.1 Overview
  - 1.2 Functionalities
- 2 Architecture
- 3 Technology Stack

# NationalLandmarks

NationalLandmarks is my defense project for Softuni Angular course.  
The software is powered by C#/.NET 6 back-end and supports MSSQL database. The front-end part is implemented via Angular and RxJS. The project overview, functionalities, architecture and technology stack is reviewed bellow.  
![Screenshot](readme_img.png?raw=true "NationalLandmarks")

## :pencil: Overview

If you do not register, you will not be able to create, visit are rate landmarks, regarding if its national or not.  
### How can you register?
Register online in a few simple steps by visiting the website page. Submit your:  
:pushpin: First Name  
:pushpin: Last Name  
:pushpin: User Name  
:pushpin: E-mail  
:pushpin: Password  

After you register successfully, you will become a user and you will be able to:  
:pushpin: Create your own landmarks  
:pushpin: Manage your own landmarks  
:pushpin: Visit your's and other user's landmarks  
:pushpin: Rate your's and other user's landmarks  

## :computer: Functionalities
### :pushpin: Landmarks Page  
On this page you can see the full list of landmarks. Here, if the landmark is created by the current user, he or she can edin and delete the created landmark. It's public too.  
### :pushpin: Create Landmark Page  
On this page you can submit new Landmark objects to the database. Accessible only for registered users.  
### :pushpin: Visited Landmarks Page
On this page you can see the landmarks, visited by the current user, his rate and date of visit. Accessible only for registered users.  
### :pushpin: Landmarks Details Page - Using Geolocation API
On this page you can read summary for landmark of your choise and some other details. Here, you can also rate and visit the place, only if you are in the current spot, where the landmark it is located.  
### :pushpin: Landmarks Edit Page
On this page you can edit landmark that you have created. Accessible only for registered users and for the landmarks, created by the user.  

# :hammer: Architecture
The project architecture is accomplished using modern approaches in web development. The front-end part is a single page application (SPA) and it is interacting with back-end API via RESTfull services, utilizing the folder-by-feature architecture.  
The authentication is custom made with JWT tokens, and also have authorization and authentication guards on the front-end and the back-end.  

# :gear: Technology Stack
- Angular, RxJS, localStorage, JWT, Geolocation API, HTML, CSS, Bootstrap  
- C# and .NET 6 
- MSSQL Server 