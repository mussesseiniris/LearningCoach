import { useState, useEffect } from "react";
import "./CreateSubject.css";

//Displays subjects.
function ShowSubjects({subjects}) {

  return (
    <div className="ShowSubjects">
      <h3 className="section-title">Subjects</h3>
     
      {/* Show subjects */}
      {subjects.map((subject, index) => (
        <div key={subject.Id} className="subject-card">
          <div id="subjectTitle">
            {index + 1}.Subject: {subject.name}
          </div>
          <p>◉ Goal:{subject.goal}</p>
          <p>◉ Start time:{new Date(subject.createdAt).toLocaleDateString()}</p>
          <p>◉ Deadline:{new Date(subject.deadline).toLocaleDateString()}</p>
        </div>
      ))}
    </div>
  );
}
export default ShowSubjects;
