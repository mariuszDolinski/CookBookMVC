<h3>CookBook App (in progress)</h3>
CookBook app for manage recipes, ingridients and units.
It is written using design patterns: Clean Architecture and CQRS (with Mediator).

<h4>Tech Stack</h4>
<ul>
<li>.Net 7.0</li>
<li>ASP.Net MVC</li>
<li>Entity Framework 7.0.5</li>
<li>Microsoft AspNetCore Identity 7.0.5</li>
<li>C#, JavaScript, jQuery, HTML, CSS</li>
<li>Bootstrap 5.0</li>
<li>SQL Server</li>
</ul>

<p>
Functions (in progress):
<ol>
<li>Unregistered users can only search recipes and view their details.</li>
<li>Registered and logged in users can create recipes and edit/remove the ones they have created. In addition they can create new ingridients and units.</li>
<li>Users with Manager role can additionally edit all recipes and also edit and remove ingridients and units (remove only if ingridient/unit is not a part of any recipe).</li>
</ol>
</p>

<p>
Current release version is available on Azure cloud: https://cookbook-mvc.azurewebsites.net<br/>
<strong>If you don't want to create new account to test app, you can use below data to logged in as testUser.</strong><br/>
Username: testUser<br/>
Password: Abcd123,
</p>
