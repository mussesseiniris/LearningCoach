import { useState } from 'react'
import './App.css'
import ReactMarkdown from "react-markdown"
import book from "./assets/book1.png";
import CreateSubject from './components/CreateSubject';
import ShowSubjects from './components/ShowSubjects';
import CreateLearningSessions from './components/CreateLearningSessions';
import ShowLS from './components/ShowLS';

function App() {
  const [message, setMessage] = useState("");
  const [response, setResponse] = useState("");
  const[chatHistory,setChatHistory]=useState([]);
  const [status,setStatus]=useState("");
  const [showCreateSub,setShowCreateSub]=useState(false);
  const [showShowSub,setShowShowSub]=useState(false);
  const [showCreateLS,setShowCreateLS] = useState(false);
  const [showLS,setShowLS]=useState(false);
  
  


  async function handleSend(){
    if(!message){
      alert("Please inout prompt!")
      return
    }
    setStatus("Sending...");
    const userMessage = {role:"User",content:message}
     setChatHistory((prev) => [...prev, userMessage]);

    try{
    const result = await fetch ("http://localhost:5138/api/AI/ask",{
      method : "POST",
      headers :{
        "Content-Type":"application/json"},
        body: JSON.stringify(message), })

  const data = await result.text();
  setResponse(data);
  setStatus("success sent");
  setMessage("");
    const AIMessage = {role:"Learning Coach",content:data}
     setChatHistory((prev) => [...prev, AIMessage]);
      } catch (error) {
      console.error("Error in handleSend:", error);
      setStatus("Failed to send message. Please try again.");
    }
  }
  
  
  return (
    <div className='HomePage'>
      <div className='menu'>
      <button onClick={()=>setShowCreateSub(!showCreateSub)}>+Create new Subject</button>
      <button onClick={()=>setShowShowSub(!showShowSub)}>Show subjects</button>
      <button onClick={()=>setShowCreateLS(!showCreateLS)}>+Learning session</button>
      <button onClick={()=>setShowLS(!showLS)}>Show Learning session</button>
      
     </div>
        {showCreateSub&&<CreateSubject/>}
        {showShowSub && <ShowSubjects/>}
        {showCreateLS&&<CreateLearningSessions/>}
        {showLS && <ShowLS/>}

    <div className="app">
  <div className='title'>
    <h1>Learning Coach</h1>
    </div>
   <div className={chatHistory.length===0?"resultEmpty":"resultArea"}>
    
     {chatHistory.length===0 ? (<img src={book} alt='book' className='homepageimg'/>):(
    chatHistory.map((message,i)=>(<div key={i}><strong>{message.role}</strong>
     <ReactMarkdown>{message.content}</ReactMarkdown> 
     </div>)))}
     {/* <ReactMarkdown>{response}</ReactMarkdown> */}
    
     </div>
     <p className='status'>{status}</p>
    <div className='inputArea'>
      <textarea type="text" value={message} onChange={(e)=>setMessage(e.target.value)} placeholder='Asking your learning coach' />
   <button onClick={handleSend}>Send</button>
     
   </div>
   </div>
   </div>
  )
}

export default App
