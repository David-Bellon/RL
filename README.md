# Q-Learning. Frozen Lake
First of all we need to know what reinforcement learning is. In normal conditions if we want to train a neural network or a simple machine learning problem we typically have data to compare the outputs with the real values and then adjust it to improve the results. In RL we don't have that so we need a solution around that.  
That solution its gonna be simple and a very efficient way of learning. If you do something wrong we penalize you and if you do it right then we give you a reward. Just as a human learn, fuck it up once so you don't fall twice.  
# Q-Learning Algorithm
What we are gonna use it's called Q-Learning and it goes like this. We have an agent that lives in a state and can perform some actions. As the agent picks an action it moves to another state and it's given a reward depending if the action was right or not. We keep this as long as we want and in the end the agent will learn which actions are the good ones and hopefully completes the task it was given. <br /> <br />
Ok, this sounds easy but we need some math to do this, not to much. So we have a bunch of states that the agent can live in and for each state the agent can do some actions, the same possible actions in every state.
So we can do this in a simple way with a matrix. The rows are gonna be the states and each column for each action. Something like this.
```math
\left(\begin{array}{cc} 
Q(s_{0}, a_{0}) & Q(s_{0}, a_{1}) & Q(s_{0}, a_{2}) & . & . & . & Q(s_{0}, a_{m})
\\ Q(s_{1}, a_{0}) & Q(s_{1}, a_{1}) & Q(s_{1}, a_{2}) & . & . & . & Q(s_{1}, a_{m})
\\ . & . & .  & . & . & . & .
\\ . & . & .  & . & . & . & .
\\ . & . & .  & . & . & . & .
\\ Q(s_{n}, a_{0}) & Q(s_{n}, a_{1}) & Q(s_{n}, a_{2}) & . & . & . & Q(s_{n}, a_{m})
\end{array}\right)
```
At the beginning this matrix is going to be initialize with all zeros so we need something to change this and update the matrix as the agent pick actions. The thing we need is more math yep, sorry.
```math
Q(s, a) = Q(s, a) + \alpha * (r + \gamma * Max(Q(s', a')) - Q(s, a))
```
I know, you understand nothing but no worries, me neither. In basic terms, you are in a state (A row of our matrix) and pick an action (select a column of that row) and as me saw before you end in another state (another row).  
So to change the value of the previous row and column we take that same value and add the thing on the right, the $\alpha$ is the learning rate (A number between 0 and 1) the $\gamma$ is the discount factor (Has to be in the same range as the learning rate) and the last thing is the max value of the row we arrive after picking an action. <br /> <br />
And thats it, you let that run as long as you want and it works. But to see this in a better way lets take a visual example.
# Frozen Lake in Unity
Lets take a simple problem, the frozen lake. You are in a lake with holes, if you fall into one you go back to the start. Your goal is to reach the end without falling, simple. <br /> <br />
Let's see an example I made in unity so you can have a better look:
![Captura de pantalla 2023-09-22 183531](https://github.com/David-Bellon/RL/assets/91338053/c3d33ad4-2225-4cd9-b1d9-d4ba8a5c010e)
You are the white agent, the dark blue squares are holes, the other ones are safe ground and the yellow is your goal.  
You may need a little knowledge of unity to replicate this UI but the core is the code that I provide. <br /> <br />
### Epsilon Decay
The next thing is simply let it run but before this I have to tell you something. How the agent picks the action? Well it's easy, pick the highest value of the action in the current state, if they are zero or are the same pick a random one.  
The problem with this is that once it finds a path it will never pick other route, if we want to let it explore we simply do something call epsilon decay. This means that at the first iterations the agent will pick random actions and as the iterations increase it will pick the highest value.
## Incredible Demostration
I let him go for 100 iterations, in the simulation you can see the iteration number and the number of wins/fails as well as the state matrix every step.  
Here you have the one with the Epsilon Decay and the other one without so you can compere. Enjoy.
### No Epsilon Decay
