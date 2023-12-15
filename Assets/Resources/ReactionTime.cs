using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class ReactionTime {
	private List<float> reactionTimes = new List<float>();

	public int getCount() {
		return reactionTimes.Count();
	}

	public float getLast() {
		return reactionTimes.Last();
	}

	public void add(float reactionTime) {
		reactionTimes.Add(reactionTime);
	}

	public float getMinimum() {
		return reactionTimes.Min();
	}

	public float getAverage() {
		return reactionTimes.Average();
	}

	public float getMaximum() {
		return reactionTimes.Max();
	}
}