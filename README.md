# OMT-Api-ASP

## Endpoints

### Employees

#### "/employees"

GET All Employees  
AUTHORIZATION = Must be logged in

#### "/employees/id"

GET Employee by Id  
AUTHORIZATION = Must be logged in

#### "/employees"

POST create employee (register)  
AUTHORIZATION = ADMIN

#### "/employees/login"

POST login  
returns JWT token with expiry time 9 hrs

#### "/employees/id"

PATCH partially update single employee  
AUTHORIZATION = ADMIN

#### "/employees/change-password/id"

PATCH change password for a single employeee  
AUTHORIZATION = Only logged in user can change his password.  
NOTE - admin can change password for all users(including other admins) via "/employees/id" PATCH

#### "/employees/id"

DELETE single user.  
AUTHORIZATION = ADMIN
