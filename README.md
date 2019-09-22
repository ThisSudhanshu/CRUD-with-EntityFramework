# CRUD-with-EntityFrameWork
This is a demo of a simple crud application is implemented on a large database of over 1000 entries. 
I have used Visual Studio 2019.

This API uses LHS Square Bracket operator in request like [lt],[gt],etc

The database consists of 8 columns, which are:
1.ID
2.Name
3.category
4.Price
5.Brand
6.Colour
7.Size
8.Weight



An example for request is -
api/products?skip=0&take=20&sortColumn=Weight&sortDirection=asc&sortColumn1=Weight&Colour[like]=mar&Weight[gt]=500
