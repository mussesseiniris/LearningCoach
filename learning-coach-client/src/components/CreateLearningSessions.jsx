import { useEffect, useState } from "react";
import "./CreateLearningSessions.css";

//  Handle creating new learning sessions.
function CreateLearningSessions() {
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");
  const [subjects, setSubjects] = useState([]);
  const [subjectId, setSubjectId] = useState(0);
  const [note, setNote] = useState("");

  //Fetch all subjects once on mount.
  useEffect(() => {
    async function fetchSubjects() {
      const res = await fetch("http://localhost:5138/api/Subject");
      const data = await res.json();
      setSubjects(data);
    }
    fetchSubjects();
  }, []);

  //Calculate duration and post new learning session to the backend.
  async function handleCreateLS() {
    const duration = (new Date(endTime) - new Date(startTime)) / 60000;

    const result = await fetch("http://localhost:5138/api/LearningSession", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        UserId: 1,
        StartTime: startTime + ":00Z",
        EndTime: endTime + ":00Z",
        Duration: duration,
        subjectId: subjectId,
        Note: note,
      }),
    });
    if (result.ok) {
      alert("Learning Session created!");
    }
  }

  return (
    <div className="CreateLearningSessions">
      <div className="section-title">
        {" "}
        <h3>New Session</h3>
      </div>

      <div className="card">
        <h3>Subject</h3>
        
        {/* Dropdown to select a subject. */}
        <select
          value={subjectId}
          onChange={(e) => setSubjectId(e.target.value)}
        >
          <option>Select a subject</option>
          {subjects.map((s) => (
            <option value={s.id} key={s.id}>
              {s.name}
            </option>
          ))}
        </select>
      </div>
      
      {/* Enter needed content */}
      <div className="card">
        <h3>Start time</h3>
        <input
          type="datetime-local"
          value={startTime}
          onChange={(e) => setStartTime(e.target.value)}
        />
      </div>

      <div className="card">
        <h3>End time</h3>
        <input
          type="datetime-local"
          value={endTime}
          onChange={(e) => setEndTime(e.target.value)}
        />
      </div>

      <div className="card">
        {" "}
        <h3>Note</h3>
        <textarea
          id="Note"
          value={note}
          onChange={(e) => setNote(e.target.value)}
        ></textarea>
      </div>

      <button onClick={handleCreateLS}>Create</button>
    </div>
  );
}
export default CreateLearningSessions;
