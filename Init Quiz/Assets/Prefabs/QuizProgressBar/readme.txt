1. Drag Prefab/QuizProgressBar.prefab to scene & align it
2. QuizProgressBar component :
	- MaxItemCount: set maximum item count (default is 10)
	- InitOnAwake:
		.true: auto-initialize progress bar in Awake function
		.false: you must call QuizProgressBar.Instance.init(MaxItemCount) before use progress bar 
			(Note: sure that the progress bar was activated at least once to make QuizProgressBar.Instance != null)
			
	- Call QuizProgressBar.Instance.notifyQuizResult(int paramIndex, bool paramCorrect) to update answer result
	- Call QuizProgressBar.Instance.notifyQuizChanged(int paramNewIndex) to update current quiz index