import { useState, useEffect } from "react";
import "./ShowLS.css";

export default function ShowSche({ onGenerate, subjects }) {
  const [subjectId, setSubjectId] = useState(0);
  const [learningSessions, setLSs] = useState([]);
  const sysPrompt =
    "Hi Claude! Could you create learning timelines? Please create a learning timeline. Use GFM task list format for each day, like this: - [ ] Day 1 - Activity name   - [ ] Day 2 - Activity name. If you need more information from me, ask me 1-2 key questions right away. If you think I should give you more context or upload anything to help you do a better job, let me know. Use any tools you have access to—like Google Drive, web search, etc.—if they'll help.If it makes sense, create something we can look at together—like a visual, a checklist, or something interactive. Thanks for your help!";
const token = localStorage.getItem("token");
  // Fetch learning sessions whenever subjectId changes

  async function handleGetLS() {
    try {
      var result = await fetch(
        "http://localhost:5138/api/LearningSession?subjectId=" + subjectId,
        {
          method: "Get",
          headers: {
            "Authorization":`bearer ${token}`,
             "Content-Type": "application/json" },
        },
      );
      var data = await result.json();
      setLSs(data);
    } catch (error) {
      console.error(error);
    }
  }
  useEffect(() => {
    handleGetLS();
  }, [subjectId]);

  function handleGenerate() {
    const selectedSubject = subjects?.find((s) => s.id == subjectId);
    const now = new Date();
    const deadline = new Date(selectedSubject?.deadline);
    const daysRemaining = Math.ceil((deadline - now) / (1000 * 60 * 60 * 24));
    const prompt = `
Subject: ${selectedSubject?.name}

DaysRemaining: ${daysRemaining}

My learning sessions:

${learningSessions.map((s) => `- ${s.note} (${s.duration} mins)`).join("\n")}

${sysPrompt}`;
    onGenerate(prompt);
  }
  return (
    <div className="ShowSche">
      <h3>Select a subject:</h3>

      {/* Dropdown to select a subject */}
      <select value={subjectId} onChange={(e) => setSubjectId(e.target.value)}>
        <option>Select a subject</option>
        {(subjects ?? []).map((sub, index) => (
          <option value={sub.id} key={sub.id}>
            {index + 1}.{sub.name}
          </option>
        ))}
      </select>
      <div>
        <button onClick={() => handleGenerate(prompt)}>Generate</button>
      </div>
    </div>
  );
}
