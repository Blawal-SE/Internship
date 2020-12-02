Feature: DeleteStudent

@mytag
Scenario:DeleteStudent
	Given the user is  login
	And the Id of Student to delete is 99
	When the Student of that Id get and Request to Delete record Made
	Then the result will  be true