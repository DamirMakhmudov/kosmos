# KOSMOS API documentaion

Repository contains the documentation for KOSMOS REST API based on Technical Data Management System (TDMS)

## Contains
- [Overview](#overview)
- [Create User](#CreateUser-post)
- [Update User](#UpdateUser-post)

## Overview

Use your host as origin url. Make sure `http` or `https` protocol  from your administrator

```
https://your_host/api
```

For example:

```
http://tdms-srv-virt:444/api
```

For `POST` request set `mode` key to request body to specify inner method your invoke. 'mode' value isn't case sensitive.

## CreateUser `POST`

Creates new TDMS User. Returns TDMS User's sysname. TDMS Users's login will be generated automatically in order to the rule:
`{lastname}{firstname}`

### Request:
```
Content-Type: application/json
```
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
|mode               |string|**true**|name of method your invoke
|user               |object|**true**|object user contains TDMS user's properties
|user.firstname     |string |**true**|TDMS users's firstname
|user.lastname      |string |**true**|TDMS users's lastname
|user.patronymic    |string |false   |TDMS users's patronymic
|user.password      |string |false   |TDMS users's password
|user.phone         |string |false   |TDMS users's phone
|user.email         |string |false   |TDMS users's email

### Response
```
Status: 200
Content-Type: text/plain; charset=UTF-8
```
TDMS User's sysname:
```
USER_5CC201F2_3306_4A92_A2D1_65FAD0E25916
```
Possible errors:

|Error code    |Description
|-             |-
|400 BadRequest|Any error
|404 NotFound  |Method {mode} not found
|404 NotFound  |Invalid 'mode' value. Should be 'createuser', 'updateuser' etc.
|404 NotFound  |Parameter 'firstname' not found
|404 NotFound  |Parameter 'lastname' not found

---
## UpdateUser `POST`

Updates attributes for existring TDMS User

### Request:
```
Content-Type: application/json
```
```json
{
    "mode": "UpdateUser",
    "user": {
        "sysname": "USER_50DC2BB0_C6F7_4467_B0DA_8C24DAC292D7",
        "login": "",
        "password": "",
        "firstname": "Вася",
        "lastname": "Пупкин",
        "patronymic": "Михайлович",
        "phone": "89274433788",
        "email": "vpupkin@gmail.com",
        "position": "NODE_C9A2678A_A99B_4BF1_AD81_C497ACBF2DF9",
        "department": "NODE_83E68FBA_0D8A_4D3C_8F82_95161D5BBCA5"
    }
```

|Parameter      |Type  |Required|Description
|-              |-     |-       |-               
|mode           |string|**true**|name of method your invoke
|user           |object|**true**|object user contains TDMS user's properties
|user.sysname   |string|**true**|TDMS users's sysname
|user.login     |string|false   |TDMS users's login
|user.firstname |string|false   |TDMS users's firstname
|user.lastname  |string|false   |TDMS users's lastname
|user.patronymic|string|false   |TDMS users's patronymic
|user.password  |string|false   |TDMS users's password
|user.phone     |string|false   |TDMS users's phone
|user.email     |string|false   |TDMS users's email
|user.position  |string|false   |TDMS Position Classifier's sysname
|user.department|string|false   |TDMS Department Classifier's sysname

### Response
```
Status: 200
Content-Type: text/plain; charset=UTF-8
```
Status name:
```
ok
```
Possible errors:

|Error code    |Description
|-             |-
|400 BadRequest|Any error
|404 NotFound  |Method {mode} not found
|404 NotFound  |Invalid 'mode' value. Should be 'createuser', 'updateuser' etc.
|404 NotFound  |TDMS User sysname = {sysname} not found";
