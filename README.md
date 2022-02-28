# Gringotts Bank API
Gringotts Bank - Secured by Goblins 
### [https://gringottsbankapi.azurewebsites.net](https://gringottsbankapi.azurewebsites.net/swagger/index.html)

## Application Design

The application is mainly divided into 3 layers - Controller, Process Layer and Data Layer

1. Controller: Responsible for receiving inputs, validating inputs and share response.
2. Process Layer: Responsible for handling the logic part of a workflow.
3. Data Layer: Responsible for interaction with database.

Other Actors:
1. Service Model: Define the models for entities for the response.
2. Domain Model: Define the class models for the database tables.
3. Utility: Manages the extension class, constants and mapping responsibilities.

Database: Postgres (deployed on Heroku).

![Application Design](https://user-images.githubusercontent.com/8267052/155964398-c640c948-bedd-491c-a4de-17845a343448.png)

# Authentication

The JWT bearer token is used for the authentication of endpoints.

## Generate JWT Token

### Endpoint: /Admin/GenerateToken

1. Use secret key **getir** to generate bearer token. 
2. Copy the bearer token from response.
3. Click on the authorize button on the top right of swagger page.
4. Paste the bearer token in the text box and click **Authorize**.

## Customer EndPoints

### POST /Customer/Add
Use this endpoint to add a new customer.

#### Parameters
1. firstname: The first name of customer.
2. lastname: The last name of customer.
3. email: The email of customer.

### GET /Customer/Get
Use this endpoint to fetch details of a customer.

#### Parameters
1. customerId: The id of customer.

## Account EndPoints

### POST /Account/Add
Use this endpoint to open a new account for a customer.

#### Parameters
1. type: 0 for Saving account & 1 for Current account.
2. amount: The amount to deposit in new account. Minimum 1 USD is required.
3. customerId: The id of customer.

### GET /Account/Get
Use this endpoint to fetch details of an account.

#### Parameters
1. accountId: The id of account or account number.

### GET /Account/GetCustomerAccounts
Use this endpoint to fetch details of all the accounts of a customer.

#### Parameters
1. customerId: The id of customer.
2. skip: Skip the number of account from the top, default is 0.
3. take: Return number of account for a customer, default is 10.

## Transaction EndPoints

### POST /Transaction/Deposit
Use this endpoint to deposit money in account.

#### Parameters
1. amount: The amount to deposit in account. Minimum 1 USD is required.
2. accountId: The id of account or account number.

### POST /Transaction/Withdraw
Use this endpoint to withdraw money from account.

#### Parameters
1. amount: The amount to withdraw from account. Minimum 1 USD is required.
2. accountId: The id of account or account number.

### GET /Transaction/GetAllTransactions
Use this endpoint to fetch transaction details of an account.

#### Parameters
1. accountId: The id of account or account number.
2. skip: Skip the number of transactions from the top, default is 0.
3. take: Return number of transactions of an account, default is 10.

### GET /Transaction/GetAllTransactionsBetweenDate
Use this endpoint to fetch transaction details of an account between a date range.

#### Parameters
1. accountId: The id of account or account number.
2. startDate: Returns transactions occurred on or after this date.
3. endDate: Returns transactions occurred before this date.
4. skip: Skip the number of transactions from the top, default is 0.
5. take: Return number of transactions of an account, default is 10.

## Sample Customer Detail for End Points

Property | Value
--- | --- 
**CustomerID** | `1014` 
**First Name** | `John`
**Last Name** | `Wick`
**Email** | `john@wick.com`

**Account Details**

Property | Value
--- | --- 
**AccountID** | `100006`
**Type** | `Saving`

Property | Value
--- | --- 
**AccountID** | `100007`
**Type** | `Current`

## Scope of Improvement
1. Logging: logging to track the changes can be implemented to keep track of modifications.
2. ORM: We can reduce the number of line of codes to perform operations with database by using an ORM instead of standard SQL code.
