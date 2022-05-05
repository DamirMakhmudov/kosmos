# KOSMOS API documentaion

Repository contains the documentation for KOSMOS REST API based on Technical Data Management System (TDMS)

## Contains
- [Overview](#overview)
- [User](#user)

## Overview

Use your host as origin url. Make sure `http` or `https` protocol  from your administrator

`https://your_host/api`

For example:

`http://damirasus:444/api`

For `POST` request set `mode` key to request body to specify inner method your invoke

## User

Section consists methods to manage TDMS object user

### Create `POST`

Request body:
```json
{
"mode": "CreateUser",
    "user": {
        "firstname": "Вася",
        "lastname": "Пупкин",
        "patronymic": "Михайлович",
        "password": "",
        "phone": "89271234567",
        "email": "vpupkin@gmail.com"
    }
}
```

|Parameter          |Type  |Required|Description
|-                  |-     |-       |-               
|`mode`            |string|true    |name of method your invoke
|`user`            |object|true    |object user contains TDMS user's properties
|`user.firstname` |sting |false   |TDMS users's firstname
|`user.lastname`  |sting |false   |TDMS users's lastname
|`user.patronymic`|sting |false   |TDMS users's patronymic
|`user.password`  |sting |false   |TDMS users's password
|`user.phone`     |sting |false   |TDMS users's phone
|`user.email`     |sting |false   |TDMS users's email



