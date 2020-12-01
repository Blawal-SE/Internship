Feature: EditStudentFeatures

@mytag
Scenario:Edit Student
	Given the user is login
	And the user Enter Edit Student of Id 98 with some EditRecordDetaol
	When the Request to Edit record Made
	Then the result should be true