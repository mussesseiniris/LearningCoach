import "./ShowLS.css";
import { useState, useEffect } from "react";

// Displays learning sessions filtered by subject.
// Fetches subjects for the dropdown and sessions based on selected subjectId.
function ShowLS() {
  const [learningSessions, setLearningSessions] = useState([]);
  const [subjects, setSubjects] = useState([]);
  const [subjectId, setSubjectId] = useState(0);
  const [displayCount, setDisplayCount] = useState(3);

  // Fetch learning sessions whenever subjectId changes
  useEffect(() => {
    async function handleGetLS() {
      console.log(
        "fetching:",
        "http://localhost:5138/api/LearningSession?subjectId=" + subjectId,
      );
      var result = await fetch(
        "http://localhost:5138/api/LearningSession?subjectId=" + subjectId,
        {
          method: "Get",
          headers: { "Content-Type": "application/json" },
        },
      );
      var data = await result.json();
      setLearningSessions(data);
    }

    handleGetLS();
  }, [subjectId]);

  // Fetch all subjects once on mount for the dropdown
  useEffect(() => {
    async function fetchSubject() {
      var subres = await fetch("http://localhost:5138/api/Subject", {
        method: "Get",
        headers: { "Content-Type": "application/json" },
      });
      var subdata = await subres.json();
      setSubjects(subdata);
    }
    fetchSubject();
  }, []);

  return (
    <div className="ShowLearningSessions">
      <div className="section-title">
        <h3>Lerning Sessions</h3>
      </div>
      <h3>Select a subject:</h3>

      {/* Dropdown to select a subject */}
      <select value={subjectId} onChange={(e) => setSubjectId(e.target.value)}>
        <option>Select a subject</option>
        {subjects.map((sub, index) => (
          <option value={sub.id} key={sub.id}>
            {index + 1}.{sub.name}
          </option>
        ))}
      </select>

      {/* Show learning sessions based on chosen subject */}
      {learningSessions.slice(0, displayCount).map((ls, index) => (
        <div key={ls.id} className="ls-card">
          <h3>Learning Session {index + 1}</h3>
          <p>◉ Start time: {ls.startTime}</p>
          <p>◉ Duration: {ls.duration} mins</p>
          <p>◉ Note: {ls.note}</p>
        </div>
      ))}

      {/* Show 'More' button or "no more learning sessions" message */}
      {displayCount >= learningSessions.length ? (
        <div className="hint">No more learning sessions</div>
      ) : (
        <button onClick={() => setDisplayCount(displayCount + 3)}>More</button>
      )}
    </div>
  );
}
export default ShowLS;
