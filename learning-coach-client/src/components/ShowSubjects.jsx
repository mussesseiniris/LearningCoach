import { useState,useEffect} from 'react';
import './CreateSubject.css'


function ShowSubjects(){
    const [subjects,setSubjects]=useState([]);
    useEffect(() => {
    async function  handleGetSubjects(){
        var result = await fetch ("http://localhost:5138/api/Subject",{
            method:"Get",
            headers:{"Content-Type":"application/json"},
        })
        
        var data = await result.json();
        setSubjects(data);
    }
      handleGetSubjects();
    }, []); // 这个空数组 [] 表示"只在第一次加载时执行"


    return(
        <div className='ShowSubjects'>
<h3 className='section-title'>Subjects</h3>
{subjects.map((subject,index)=>(<div key={subject.Id} className="subject-card">
    <h3>{index+1}.Subject:{subject.name}</h3>
    <p>◉ Goal:{subject.goal}</p>
    {/* <p>{subject.deadline}</p> */}
    <p>◉ Start time:{new Date(subject.createdAt).toLocaleDateString()}</p>
    <p>◉ Deadline:{new Date(subject.deadline).toLocaleDateString()}</p>
    
</div>))}

        </div>
    )
}
export default ShowSubjects;