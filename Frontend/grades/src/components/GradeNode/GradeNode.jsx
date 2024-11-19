import "./GradeNode.css";
import { useState } from "react";

export default function GradeNode({
  gradeId,
  dateTime,
  firstName,
  lastName,
  subjectName,
  gradeGot,
  onUpdate,
  onDelete,
}) {
  const [newGrade, setNewGrade] = useState(gradeGot);
  const [inputLabel, setInputLabel] = useState(false);

  const ttt = 4;
  const dateArr = dateTime.split("-");
  const date = dateArr[2].concat(".", dateArr[1], ".", dateArr[0]);

  const handleChange = (e) => {
    e.preventDefault();
    setInputLabel(true);
  };

  const handleChangeSubmit = (e) => {
    e.preventDefault();
    setInputLabel(false);
    onUpdate(gradeId, newGrade);
  };

  const handleChangeCancel = (e) => {
    e.preventDefault();
    setInputLabel(false);
  };

  return (
    <div className="grade-node">
      <label>{firstName}</label>
      <label>{lastName}</label>
      <label>{subjectName}</label>

      {inputLabel == false && <label>{gradeGot}</label>}

      {inputLabel == true && (
        <input value={newGrade} onChange={(e) => setNewGrade(e.target.value)} />
      )}

      <label>{date}</label>

      {inputLabel == false && (
        <button className="update-button" onClick={handleChange}>
          Update
        </button>
      )}
      {inputLabel == false && (
        <button className="delete-button" onClick={() => onDelete(gradeId)}>
          Delete
        </button>
      )}

      {inputLabel == true && (
        <button className="update-button" onClick={handleChangeSubmit}>
          Submit
        </button>
      )}

      {inputLabel == true && (
        <button className="delete-button" onClick={handleChangeCancel}>
          Cancel
        </button>
      )}
    </div>
  );
}
