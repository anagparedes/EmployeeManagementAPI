﻿# EmployeeAPI

## Auth
[POST] /api/Auth/Register
```json
{
  "username": "string",
  "password": "string"
}
```
[POST] /api/Auth/login
```json
{
  "username": "string",
  "password": "string"
}
```
## Employee
[GET] /api/Employee
```json
[{
  "id": 0,
  "name": "string",
  "role": "string",
  "company": "string"
}]
```
[POST] /api/Employee
```json
  {
    "id": 0,
    "name": "string",
    "role": "string",
    "company": "string"
  }
```
[PUT] /api/Employee
```json
  {
    "id": 0,
    "name": "string",
    "role": "string",
    "company": "string"
  }
```
[DELETE] /api/Employee
```json
  {
    "id": 0,
    "name": "string",
    "role": "string",
    "company": "string"
  }
```
[GET] /api/Employee/{id}
```json
 [
  {
    "id": 0,
    "name": "string",
    "role": "string",
    "company": "string"
  }
]
```
[GET] /api/Employee/search/{searchbyName}
```json
 [
  {
    "id": 0,
    "name": "string",
    "role": "string",
    "company": "string"
  }
]
```
