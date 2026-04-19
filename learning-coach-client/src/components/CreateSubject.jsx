import { useState } from "react";
import "./CreateSubject.css";
function CreateSubject({onCreated}) {
  // const [timeStart, setTimeStart] = useState("");
  // const [timeEnd, setTimeEnd] = useState("");
  const [subject, setSubject] = useState("");
  const [deadline, setDeadline] = useState("");
  const [goal, setGoal] = useState("");
  const [priority, setPriority] = useState(0);
  const [duration, setDuration] = useState(0);

  const token = localStorage.getItem("token");

  async function handleCreateSubject() {
    if (!subject || !deadline || !priority || !duration) {
      alert("Please fill in all required fields!");
      return;
    }
    try {
      const result = await fetch("/api/Subject", {
        method: "POST",
        headers: { "Authorization":`bearer ${token}`,
          "Content-Type": "application/json" },
        body: JSON.stringify({
          name: subject,
          deadline: new Date(deadline).toISOString(),
          goal: goal,
          duration: duration,
          priority: priority,
          userId: 1,
        }),
      });
      if (result.ok) {
        alert("Subject created!");
        onCreated();
      }
    } catch (error) {
      console.error(error);
    }
  }

  return (
    <div className="CreateSubject">
      <h3 className="section-title">New Subject</h3>
      {/* <h3>Subject</h3> */}
      <input
        type="text"
        placeholder="e.g. English"
        value={subject}
        onChange={(e) => setSubject(e.target.value)}
      />
      <div className="card">
        <p>Goal</p>
        <textarea
          type="text"
          value={goal}
          onChange={(e) => setGoal(e.target.value)}
        />
      </div>
      <div className="card">
        <p>Deadline</p>
        <input
          className="deadline"
          type="date"
          value={deadline}
          onChange={(e) => setDeadline(e.target.value)}
        />
      </div>
      <div className="card">
        <p>Duration</p>
        <input
          type="text"
          value={duration}
          onChange={(e) => setDuration(e.target.value)}
        />
      </div>
      <div className="card">
        <p>Priority</p>
        <input
          type="text"
          value={priority}
          onChange={(e) => setPriority(e.target.value)}
        />
      </div>
      <button onClick={handleCreateSubject}>Create</button>
    </div>
  );
}
export default CreateSubject;
