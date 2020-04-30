# GitHub Repositories Search App

This is a Web application for searching GitHub repositories by repository name.<br>
- The user types the repository he would like to search;
- The page displays results as gallery items where each item shows repository name, owner's avatar and a bookmark button;
- The user can move through the pages by clicking the “Next“ button.


The solution uses GitHub APIs to retrieve the data and responsive web design patterns to render the result.


### Technologies : AngularJS, ASP.NET Core, Bootstrap



## Server-side - ASP.NET Core API project


- Makes request to the GitHub API to retrieve information.

**References**:
[Search API helps to search for the specific item](https://developer.github.com/v3/search/#search-repositories)<br>
[Traversing with Pagination](https://developer.github.com/v3/guides/traversing-with-pagination/)

- Allows user to bookmark a repository by storing the entire result to the user's session.

**Guides, links**:
[Session and app state in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-2.2)<br>
[Using Sessions and HttpContext in ASP.NET Core and MVC Core](https://benjii.me/2016/07/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core/)


## Client-side - SPA created with AngularJS and Bootstrap library


- For simplicity, ASP.NET Core serves static HTML, CSS, and JavaScript files directly to clients.<br>
The files are stored within the project's web root directory.

- Each page displays 30 records from found GitHub repositories.<br>
User can move through the pages by clicking the “Next“ button on the bottom of the screen.

#

### How to run the project:

1. Open the project from Visual Studio 2017

2. Run *dotnet restore* in NuGet Package Manager Console

3. In case of missing “Microsoft.AspNetCore.Session” package, install the stable version of the package from the NuGet Package Manager

4. Rebuild the project and then run it


