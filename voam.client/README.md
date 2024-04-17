# Voam client

## Application development description

1. Developed with Visual Studio Code v.1.76.2 + Node.js v.16.14.2.
2. Used libs: React.js v.18.2.0 + Vite v.4.4.5 with HMR, Babel plugin for fast refresh and some ESLint v.8.45.0 rules.
3. Browsers used: Google Chrome(latest versions)+ Addons: React Dev.Tools, JSON Formatter.

## Usage

1. **Navigation bar:**
   Navigation links change the current page (view). Guests (unauthenticated visitors) see the links to the Home, Items, About, Contact, as well as the links to the Login and Register pages.
   The logged-in user navbar contains the links to Home, Items, About, Contact, Profile, Shopping Cart, Logout pages.
   
2. **Login User:**
    The Login page is available only for guests (unauthenticated visitors).

    The included REST service comes with the following premade user accounts, which you may use for test purposes:
    ```json
	{ "email": "admin@mail.com", "password": "Vo@m2024" }
	{ "email": "kosebose@mail.com", "password": "Kz123" }
    ```
	
	**REST Service API Endpoint:**
	-   _Method: POST_
	-   _Request body:_
    ```json
    { 
       "email": "string",
       "password": "string"
    }
    ```
	-   _URL: https://localhost:7097/api/Auth/Login_

	The Login page contains a form for existing user authentication. By providing an email and password, the app login user in the system if there are no empty fields.
	Upon success, the REST service returns information about the existing user along with a property accessToken, which contains the session token for the user, in order to be able to perform authenticated requests.
	After successful login, the user is redirected to the Home page. If there is an error, an appropriate error message is displayed.

3.	**Register User:**
    The Register page is available only for guests (unauthenticated visitors).

	**REST Service API Endpoint:**
	-   _Method: POST_
	-   _Request body:_
    ```json
    { 
       "email": "string",
       "password": "string",
       "fistName": "string",
       "lastName": "string",
       "phoneNumber": "string"
    }
    ```
	-   _URL: https://localhost:7097/api/Auth/Register_

	The Register page contains a form for new user registration. By providing an email, password, first name, last name and phoneNumber, the app register new user in the system if there are no empty fields.
	Upon success the REST service returns the newly created object with an automatically generated id and a property accessToken, which contains the session token for the user, in order to be able to perform authenticated requests.
	After successful registration, the user is redirected to the Home page. If there is an error, an appropriate error message is displayed.

4.	**Logout User:**
	The logout action is available only for logged-in users.

	-   _URL: http://localhost:7097/logout_

	After successful logout, the user is redirected to the Home page.

6.	**Home page:**
	All users are welcomed to the Home page, where they can see recently added items and proceed to the catalogue with all other books.

<ul align="center">
	<img src="https://github.com/kzaraliev/Voam/blob/main/Images/HomePage_1.png">
	<img src="https://github.com/kzaraliev/Voam/blob/main/Images/HomePage_2.png">
</ul>

6.	**Items Catalog page:**
	This page displays a list of all items on the site. Clicking on the the cards leads to the details page for the selected item.

	<p align="center">
		<img src="https://github.com/kzaraliev/Voam/blob/main/Images/ItemsPage.png">
	</p>

	**REST Service API Endpoints:**
	-   _Method: GET_ 
	-   _URL: https://localhost:7097/api/Product/GetAllProducts - for all items_

8.	**Create item page:**
	The Create book page is available to admin users. It contains a form for creating new item. Admin can publish items with send request, if there are no empty fields.
	
	**REST Service API Endpoint:**
	-   _Method: POST_
	-   _Request headers:_
	```json
	{
	   "Bearer": "accessToken",
	   "Content-Type": "application/json"
	}
    ```
	-   _Request body:_
	```json	
	{ 
	   "name": "string",
	   "description": "string",
	   "price": "decimal",
      "sizeS": "integer number",
      "sizeM": "integer number",
      "sizeL": "integer number",
       "Images": "string[]"
	}	
    ```
	-   _URL: https://localhost:7097/api/Product/CreateProduct_
	
	Upon success, the REST service returns the newly created item.

10.	**Details page:**
	All users are able to view details about items. Clicking on the the cards leads to the details page for the selected item. If the currently logged-in user is admin, the [Edit] and [Delete] buttons are displayed.
	Every logged-in user is able to add to cart the item they have opened.

	<p align="center">
		<img src="https://github.com/kzaraliev/Voam/blob/main/Images/DetailsPage.png">
	</p>

	**REST Service API Endpoints for Details view:**
	-   _Method: GET_
	-   _URL: https://localhost:7097/api/Product/GetProductById?id={productId} - for selected item_
	-   _URL: https://localhost:7097/api/Review/GetProductAverageRating?productId={productId}&userId={userId} - to find all reviews about this item_
	
	**REST Service API Endpoint for Commenting action:**
	-   _Method: POST_
	-   _Request headers:_
	   ```json
	   {
	      "Bearer": "accessToken",
	      "Content-Type": "application/json"
	   }
	   ```
	-   _URL: https://localhost:7097/api/Review/AddRating - to add a review to the item_

  Only logged-in users can add reviews. After successful rating the average rate for the products is displayed next to the stars.

11. **Edit Book:**
    The Edit page is available only to admin user. Clicking on the [Edit] button of a particular item on the Details page, redirects user to the Edit page with all fields filled with the data for the item. It contains a form with input fields for all relevant properties. The admin is able to update it by sending the correct filled form with no empty fields in it before the request making.

    **REST Service API Endpoint:**
	-   _Method: PUT_
	-   _Request headers:_
	```json
	{
	   "Bearer": "accessToken",
	   "Content-Type": "application/json"
	}
    ```
	-   _Request body:_
	```json	
	{ 
	   "name": "string",
	   "description": "string",
	   "price": "decimal",
      "sizeS": "integer number",
      "sizeM": "integer number",
      "sizeL": "integer number",
       "Images": "string[]"
	}	
    ```
	-   _URL: https://localhost:7097/api/Product/UpdateProduct?id={productId}_

    Upon success, the REST service returns the modified item.
    After successful edit request, the admin is redirected to the Details page of the currently edited item.

12. **Delete Book:**
    The delete action is available to the admin. When the author clicks on the [Delete] button of a particular item on the Details page, a confirmation dialog is displayed, and upon confirming the dialog, the product is deleted from the system.

    **REST Service API Endpoint:**
	-   _Method: DELETE_
	-   _Request headers:_
    ```json
    {
       "Bearer": "accessToken",
       "Content-Type": "application/json"
    }
    ```
	-   _URL: https://localhost:7097/api/Product/DeleteProduct?id={productId}_
    After successful delete request, the user is redirected to the Items page.

13.	**Profile page:**
	The Profile page is available only to logged-in users.
	This page displays a list of all orders made by the current user. If there are no orders yet, message is displayed along with a "Go ahead & explore." link that redirects the user to the Items page.

	<p align="center">
		<img src="https://github.com/kzaraliev/Voam/blob/main/Images/profilePage.png">
	</p>
 
	**REST Service API Endpoint:**
	-   _Method: GET_
	-   _Request headers:_
	```json
	{
	   "Bearer": "accessToken"
     "Content-Type": "application/json"
	}
    ```	
	-   _URL: https://localhost:7097/api/Order/GetAllOrdersForUser?id={userId}&pageSize={pageSize}&pageNumber={pageNumber}_

14.	**Admin page:**
  The Profile page is available only to admin users. This page displays a list of all orders and charts for statistics.

<p align="center">
		<img src="https://github.com/kzaraliev/Voam/blob/main/Images/AdminPage_1.png">
    <img src="https://github.com/kzaraliev/Voam/blob/main/Images/AdminPage_2.png">
</p>

**REST Service API Endpoint:**
	-   _Method: GET_
	-   _Request headers:_
  ```json
{
	"Bearer": "accessToken"
    "Content-Type": "application/json"
}
  ```	
  -   _URL: https://localhost:7097/api/Order/GetAllOrders_

## Project Structure

-   **`/voam.client`**: Contains the SPA.
    -   `/src`: React components, style css, and business logic, contexts, guards and requester services.
		  -	`/components`: React components, style css, and business logic.
    	-	`/context`: React AuthContext component that share authentication and authorization states between components.
    	-	`/guards`: React AuthGuard, AdminGuard and LoggedInGuard components that check the authentication and authorization of the current user.
   		-	`/hooks`: Custom React hooks.
    	-	`/utils`: Houses reusable functions, constants.
		  -	`/services`: JS logic with requester functions for the REST API Service.

## License

This project is licensed under the MIT License - see the [LICENSE](https://opensource.org/license/mit/) file for details.

