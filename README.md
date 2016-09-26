# GasMonitor

WebAPI sample for a talk on consuming REST APIs in C#

## Getting Started

This API is meant to demonstrate a few concepts around consuming JSON
APIs in C#. The concepts include

 - Authorization
 - HTTP headers
 - HTTP verbs
 - Query string parameters
 - Converting JSON to and from C# objects

The domain of this API is a back-end service for a website that lets users track
their gasoline consumption. A user (`Owner`) registers one or more `Vehicle`s with
the system. Each time they fill their tank, they enter the number of gallons of gas
they purchased and the number of miles driven on that tank. This is known as a `FillUp`
record.

### Authorization

No complicated authorization process here. All you need to do is include the
`X-Api-Key` header in each request. The expected key is stored as the 
`ApiKey` property in web.config.

```
GET /status HTTP/1.1
Host: api.example.com
Accept: application/json
X-Api-Key: 596398f21df340cb986a39ab6a117be1
```

If the key is not included or is incorrect, you will receive a `401 Unauthorized`
response.

## Response formats

This API supports XML and JSON out of the box. The default is JSON, but you
can configure this explicitly with the `Accept` header. Use `Accept: application/json`
for JSON or `Accept: text/xml` for XML.

# Resources

Further documentation can be found at the swagger documentation page: `/swagger`.

## Status

An easy way to check your client is configured correctly is to hit the
status endpoint. If you've got the right headers set up, you should get a response
with the version and uptime.

 - `GET /status` returns the current status of the server

## Owner

An owner is a user of the system who posseses one or more vehicles.

 - `POST /owners` creates a new owner
 - `GET /owners/{id}` gets the owner with GUID `id`
 - `DELETE /owners/{id}` deletes the owner with GUID `id`

## Vehicle

A Vehicle is a car or truck owner by a user. Each vehicle can be filled up with gas multiple times.
The endpoints that read vehicles return the vehicles statistics as well

 - `POST /owners/{ownerId}/vehicles` registers a new vehicle with the Owner identified by Guid `ownerId`
 - `GET /owners/{ownerId}/vehicles` gets all the vehicles belonging to the owner identified by GUID `ownerId`
 - `GET /vehicles/{id}` gets the vehicle with GUID `id`
 - `PATCH /vehicles/{id}` update the vehicle identified by GUID `id`
 - `DELETE /vehicles/{id}` remove the vehicle identified by GUID `id`

## Fillups

A `Fillup` is a recording of how much gas and how many miles the user got on a tank.

 - `POST /vehicles/{vehicleId}/fillups` records a fillup against the vehicle identified by GUID `vehicleId`
 - `GET /vehicles/{vehicleId}/fillups` Gets the fillup records against the vehicle identiied by GUID `vehicleId`
 - `GET /fillups/{id}` gets the fillup identified by GUID `id`
 - `DELETE /filups/{id}` removes the fillup identified by GUID `id`