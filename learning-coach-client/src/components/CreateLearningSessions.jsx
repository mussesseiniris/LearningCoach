import { useEffect, useState } from "react";
import "./CreateLearningSessions.css";

//  Handle creating new learning sessions.
function CreateLearningSessions({onCreated}) {
  const [startTime, setStartTime] = useState("");
  const [endTime, setEndTime] = useState("");
  const [subjects, setSubjects] = useState([]);
  const [subjectId, setSubjectId] = useState(0);
  const [note, setNote] = useState("");

  const token = localStorage.getItem("token");
  //Fetch all subjects once on mount.
  useEffect(() => {
    async function fetchSubjects() {
      try {
        const res = await fetch("http://localhost:5138/api/Subject",
          {
          headers: {
                    "Authorization": `bearer ${token}`
                }
          }
        );

        const data = await res.json();

        setSubjects(data);
      } catch (error) {
        console.error(error);
      }
    }
    fetchSubjects();
  }, []);

  //Calculate duration and post new learning session to the backend.
  async function handleCreateLS() {
       if (!subjectId || !startTime || !endTime || !note) {
      alert("Please fill in all required fields!");
      return;
    }
    const duration = (new Date(endTime) - new Date(startTime)) / 60000;
    try {
      const result = await fetch("http://localhost:5138/api/LearningSession", {
        method: "POST",
        headers: { "Authorization": `Bearer ${token}` ,
         "Content-Type": "application/json" },
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
        onCreated();
      }
    } catch (error) {
      console.error(error);
    }
  }

  return (
    <div className="CreateLearningSessions">
      <div className="section-title">
        {" "}
        <h3>New Session</h3>
      </div>

      <div className="card">
        <p>Subject</p>

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
        <p>Start time</p>
        <input
          type="datetime-local"
          value={startTime}
          onChange={(e) => setStartTime(e.target.value)}
        />
      </div>

      <div className="card">
        <p>End time</p>
        <input
          type="datetime-local"
          value={endTime}
          onChange={(e) => setEndTime(e.target.value)}
        />
      </div>

      <div className="card">
        <p>Note</p>
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
