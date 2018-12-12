Feature: CheckAuthorInYoutubeVideo

@EPMFARMATS-2466
Scenario Outline: Check author name in youtube video
	Given I navigate to <mainPage>
	Then the <authorName> should be correct

	Examples: 
| mainPage                                    | authorName |
| https://www.youtube.com/watch?v=UKKYpdWPSL8 | EPAM Systems Global|