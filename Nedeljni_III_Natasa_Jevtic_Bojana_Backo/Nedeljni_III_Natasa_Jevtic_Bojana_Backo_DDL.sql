IF DB_ID('RecipesDB') IS NULL
    create database RecipesDB;
GO	
use RecipesDB
--Deleting tables and views, if they exist
IF OBJECT_ID('dbo.tblShoppingList') IS NOT NULL DROP TABLE dbo.tblShoppingList;
IF OBJECT_ID('dbo.tblIngredients') IS NOT NULL DROP TABLE dbo.tblIngredients;
IF OBJECT_ID('dbo.tblRecipe') IS NOT NULL DROP TABLE dbo.tblRecipe;
IF OBJECT_ID('dbo.tblUser') IS NOT NULL DROP TABLE dbo.tblUser;
IF OBJECT_ID('dbo.vwShoppingList') IS NOT NULL DROP VIEW dbo.vwShoppingList;
IF OBJECT_ID('dbo.vwIngredients') IS NOT NULL DROP VIEW dbo.vwIngredients;
IF OBJECT_ID('dbo.vwRecipe') IS NOT NULL DROP VIEW dbo.vwRecipe;
IF OBJECT_ID('dbo.vwUser') IS NOT NULL DROP VIEW dbo.vwUser;
GO
create table tblUser(
UserId int identity(1,1) PRIMARY KEY,
NameAndSurname varchar(100) NOT NULL,
Username varchar(40) UNIQUE NOT NULL,
Password varchar(150) NOT NULL
);
create table tblRecipe(
RecipeId int identity(1,1) PRIMARY KEY,
RecipeName varchar(50) NOT NULL,
Type varchar(50) NOT NULL,
NumberOfPersons int NOT NULL,
Author varchar(50) NOT NULL,
Description nvarchar(250) NOT NULL,
DateOfCreation date NOT NULL,
UserId int FOREIGN KEY REFERENCES tblUser(UserId) NOT NULL
);
create table tblIngredients(
IngredientId int identity(1,1) PRIMARY KEY,
IngredientName varchar(50) NOT NULL,
Quantity decimal(8,2) NOT NULL,
RecipeId int FOREIGN KEY REFERENCES tblRecipe(RecipeId) NOT NULL
);
create table tblShoppingList(
ShoopingListId int identity(1,1) PRIMARY KEY,
UserId int FOREIGN KEY REFERENCES tblUser(UserId) NOT NULL,
RecipeId int FOREIGN KEY REFERENCES tblRecipe(RecipeId) NOT NULL
);
GO
create view vwUser as
select *
from tblUser;
GO
create view vwRecipe as
select u.UserId, u.NameAndSurname, r.RecipeId, r.RecipeName, r.Type, r.Author, r.NumberOfPersons, 
r.DateOfCreation, r.Description
from tblRecipe r
INNER JOIN tblUser u
ON u.UserId = r.UserId;
GO
create view vwIngredients as
select r.*, i.IngredientId, i.IngredientName, i.Quantity
from tblIngredients i
INNER JOIN tblRecipe r
ON i.RecipeId = r.RecipeId;
GO
create view vwShoppingList as
select u.NameAndSurname, u.UserId, r.RecipeName, i.*
from tblShoppingList s
INNER JOIN tblUser u
ON u.UserId = s.UserId
INNER JOIN tblRecipe r
ON r.RecipeId = s.RecipeId
INNER JOIN tblIngredients i
ON i.RecipeId = r.RecipeId;