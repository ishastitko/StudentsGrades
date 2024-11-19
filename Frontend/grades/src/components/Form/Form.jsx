import "./Form.css";
import { useState } from "react";

export default function Form({onCreate}) {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [subjectName, setSubjectName] = useState("");
  const [gradeGot, setGradeGot] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    onCreate(firstName, lastName, subjectName, gradeGot);
    setFirstName("");
    setLastName("");
    setSubjectName("");
    setGradeGot("");
  };

  return (
    <div className="node-form">
      <form
        class="grade-form"
        onSubmit={handleSubmit}
      >
        <input
          placeholder="First Name"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
        />
        <input
          placeholder="Last Name"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
        />
        <input
          placeholder="Subject"
          value={subjectName}
          onChange={(e) => setSubjectName(e.target.value)}
        />
        <input
          placeholder="Grade"
          value={gradeGot}
          onChange={(e) => setGradeGot(e.target.value)}
        />
        <button type="submit">Submit</button>
      </form>
    </div>
  );
}