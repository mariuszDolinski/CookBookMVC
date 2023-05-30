<h3>CookBook App (in progress)</h3>
This CookBook app is for manage recipes and also ingridients and units.
It is written using design patterns: Clean Architecture and CQRS (with Mediator).
Its current verion is available on Azure cloud: https://cookbook-mvc.azurewebsites.net

<h4>Tech Stack</h4>
<ul>
<li>.Net 7.0</li>
<li>ASP.Net Web App MVC</li>
<li>Entity Framework 7.0.5</li>
<li>Microsoft AspNetCore Identity 7.0.5</li>
<li>SQL Server</li>
</ul>

<p>
Functions:
<ol>
<li>Unregistered users can only search recipes and view their details.</li>
<li>Registered and logged in users can create recipes and edit the ones they have created. also they can create new ingridients and units.</li>
<li>Logged in users with role Manager can additionally edit all recipes.</li>
</ol>
</p>
<p>
<strong>If you don't want to create new account to test app, you can use below data to logged in as testUser.</strong>
Username: testUser
Password: Abcd123,
</p>
