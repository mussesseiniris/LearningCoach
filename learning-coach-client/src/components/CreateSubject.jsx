   import { useState } from "react";
   import './CreateSubject.css';
  function CreateSubject(){
    
  // const [timeStart, setTimeStart] = useState("");
  // const [timeEnd, setTimeEnd] = useState("");
  const [subject,setSubject]=useState("");
  const [deadline, setDeadline] = useState("");
  const [goal,setGoal]=useState("");
  const [priority,setPriority]=useState(0);
  const[duration,setDuration]=useState(0);

  async function handleCreateSubject(){
      if(!subject || !deadline || !priority || !duration){
    alert("Please fill in all required fields!");
    return;
  }
    try{

      const result = await fetch("http://localhost:5138/api/Subject",{
        method:"POST",
        headers:{"Content-Type":"application/json"},
        body:JSON.stringify({name:subject,
          deadline:new Date(deadline).toISOString(),
          goal:goal,
          duration:duration,
          priority:priority,
          userId:1
        }),
      }
    )
if(result.ok){
  alert("Subject created!");
}
      
    }
    catch(error){
      console.log(error);
      
    }
    
  }

  return(<div className='CreateSubject'>
    <h3>Create a new Subject</h3>
        {/* <h3>Subject</h3> */}
        <input type="text" placeholder='e.g. English' value={subject} onChange={(e)=>setSubject(e.target.value)} />
     
        <h3>Goal</h3>
        <textarea type="text" value={goal} onChange={(e)=>setGoal(e.target.value)} />
        <h3>Deadline</h3>
        <input className="deadline" type="date" value={deadline} onChange={(e)=>setDeadline(e.target.value)} />
        <p>Duration</p>
        <input type="text" value={duration} onChange={(e)=>setDuration(e.target.value)}/>
         <p>Priority</p>
         <input type="text" value={priority} onChange={(e)=>setPriority(e.target.value)} />
        <button onClick={handleCreateSubject}>Create</button>
        
        
      </div>)}
      export default CreateSubject;