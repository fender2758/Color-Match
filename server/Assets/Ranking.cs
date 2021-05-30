using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;


public class Ranking : MonoBehaviour
{
    public int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;

        mDatabaseRef.Child("users").SetValueAsync(score);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public LeaderboardEntry()
    {
    }

    public LeaderboardEntry(string uid, int score)
    {
        this.uid = uid;
        this.score = score;
    }

    public Dictionary<string, Object> ToDictionary()
    {
        Dictionary<string, Object> result = new Dictionary<string, Object>();
        result["uid"] = uid;
        result["score"] = score;

        return result;
    }


    private void WriteNewScore(string userId, int score)
    {
        // Create new entry at /user-scores/$userid/$scoreid and at
        // /leaderboard/$scoreid simultaneously
        string key = mDatabase.Child("scores").Push().Key;
        LeaderBoardEntry entry = new LeaderBoardEntry(userId, score);
        Dictionary<string, Object> entryValues = entry.ToDictionary();

        Dictionary<string, Object> childUpdates = new Dictionary<string, Object>();
        childUpdates["/scores/" + key] = entryValues;
        childUpdates["/user-scores/" + userId + "/" + key] = entryValues;

        mDatabase.UpdateChildrenAsync(childUpdates);
    }

    private void AddScoreToLeaders(string email,
                               long score,
                               DatabaseReference leaderBoardRef)
    {

        leaderBoardRef.RunTransaction(mutableData => {
            List<object> leaders = mutableData.Value as List<object>;
    
      if (leaders == null)
            {
                leaders = new List<object>();
            }
            else if (mutableData.ChildrenCount >= MaxScores)
            {
                long minScore = long.MaxValue;
                object minVal = null;
                foreach (var child in leaders)
                {
                    if (!(child is Dictionary<string, object>)) continue;
                    long childScore = (long)
                                ((Dictionary<string, object>)child)["score"];
                    if (childScore < minScore)
                    {
                        minScore = childScore;
                        minVal = child;
                    }
                }
                if (minScore > score)
                {
                    // The new score is lower than the existing 5 scores, abort.
                    return TransactionResult.Abort();
                }

                // Remove the lowest score.
                leaders.Remove(minVal);
            }

            // Add the new high score.
            Dictionary<string, object> newScoreMap =
                             new Dictionary<string, object>();
            newScoreMap["score"] = score;
            newScoreMap["email"] = email;
            leaders.Add(newScoreMap);
            mutableData.Value = leaders;
            return TransactionResult.Success(mutableData);
        });
    }

    DatabaseReference database = FirebaseDatabase.DefaultInstance.RootReference;
    database.Child(_loadPath).GetValueAsync().ContinueWith(task =>
        {
        if (task.IsFaulted)
        {
            Logger.LogError("Error Database");
        }
        else if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;
            object value = snapshot.Value;
            if (null != (value as IDictionary))
            {
                dic = (IDictionary<string, object>)snapshot.Value;
            }
            else
            {
                dic = new Dictionary<string, object>();
                if (null != snapshot.Value)
                {
                    dic.Add(snapshot.Key, snapshot.Value);
                }
            }
        }
    });


}


