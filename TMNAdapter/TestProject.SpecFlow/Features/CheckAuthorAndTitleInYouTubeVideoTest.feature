Feature: CheckAuthorAndTitleInYouTubeVideoTest

@EPMFARMATS-2466
Scenario Outline: Check author name in youtube video
	Given I navigate to <mainPage>
	Then the <authorName> should be correct

	Examples: 
| mainPage                                    | authorName |
| https://www.youtube.com/watch?v=UKKYpdWPSL8 | EPAM Systems Global|

 @EPMFARMATS-2470
Scenario Outline: Check title name in youtube video
	Given I navigate to <mainPage>
	Then the <titleName> should be wrong

	Examples: 
| mainPage                                    | titleName |
| https://www.youtube.com/watch?v=UKKYpdWPSL8 | EPAM Systems Global|