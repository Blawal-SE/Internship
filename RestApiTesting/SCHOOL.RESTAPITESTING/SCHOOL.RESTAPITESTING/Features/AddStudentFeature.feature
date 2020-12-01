Feature: AddStudentFeature
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario:Login User
	Given the user name is "b761"
	And the password  is "761"
	And the grant_type is "password"
	When the login request send
	Then the In Response token type should be "bearer"

Scenario:AddStudent
	Given Will Add New Student Details Call to Add Student "test"
	Then  the result will be true