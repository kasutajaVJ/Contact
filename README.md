# Kontaktide register

## DB

Contact
- Name
- PhoneNumbers [ { number: string, type: enum, default: boolean} ]
- Emails [ { email: string, type: enum, default: boolean }]

Contact (Easy)
- Name
- PhoneNumber
- Email 

## Migration

- add field "Initials"

## API


### List (with filtering by term - search from name, phone nr, email)

[GET] /api/contacts?term={}
[{
	name,
	defaultPhoneNumber,
	defaultEmail
}]

### Create

[POST ]/api/contacts/add

### Update

[POST] /api/contacts/{id}

### Delete

[DELETE] /api/contacts/{id}

