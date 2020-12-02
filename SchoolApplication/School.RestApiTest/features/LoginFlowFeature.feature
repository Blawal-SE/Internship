Feature: LoginFlowFeature
User will enter username and password and successfully logged in to system

@mytag
Scenario:Login User
	Given the user name is "b761"
	And the password  is "761"
	And the grant_type is "password"
	When the login request send
	Then the In Response UserName password Must matches with given parameter and should get accesstoken