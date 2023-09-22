# Q-Learning. Frozen Lake
First of all we need to know what reinforcement learning is. In normal conditions if we want to train a neural network or a simple machine learning problem we typically have data to compare the outputs with the real values and then adjust it to improve the results. In RL we don't have that so we need a solution around that.  
That solution its gonna be simple and a very efficient way of learning. If you do something wrong we penalize you and if you do it right then we give you a reward. Just as a human learn, fuck it up once so you don't fall twice.  
# Q-Learning Algorithm
What we are gonna use it's called Q-Learning and it goes like this. We have an agent that lives in a state and can perform some actions. As the agent picks an action it moves to another state and it's given a reward depending if the action was right or not. We keep this as long as we want and in the end the agent will learn which actions are the good ones and hopefully completes the task it was given.  
Ok, this sounds easy but we need some math to do this, not to much. So we have a bunch of states that the agent can live in and for each state the agent can do some actions, the same possible actions in every state.
So we can do this in a simple way with a matrix. The rows are gonna be the states and each column for each action.  
```math
\left(\begin{array}{cc} 
Q(s_{0}, a_{0}) & Q(s_{0}, a_{1}) & Q(s_{0}, a_{2}) & . & . & . & Q(s_{0}, a_{m})
\\ Q(s_{1}, a_{0}) & Q(s_{1}, a_{1}) & Q(s_{1}, a_{2}) & . & . & . & Q(s_{1}, a_{m})
\end{array}\right)
```
At the beginning this matrix is going to be initialize with all zeros so we need something to change this and update the matrix as the agent pick actions.
