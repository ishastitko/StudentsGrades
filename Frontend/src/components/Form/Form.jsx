import React from 'react';
import "./Form.css";
import { useState } from "react";

export default function Form({ onCreate }) {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [subjectName, setSubjectName] = useState("");
  const [gradeGot, setGradeGot] = useState("");
  const [hasError, setHasError] = useState(true);

  const validateForm = (updateValues = {}) => {
    const fName = updateValues.firstName ?? firstName;
    const lName = updateValues.lastName ?? lastName;
    const grade = updateValues.gradeGot ?? gradeGot;

    const nameRegex = /^[A-Za-z]+$/;
    const isFirstNameValid = nameRegex.test(fName);
    const isLastNameValid = nameRegex.test(lName);
    const isGradeValid = grade >= 1 && grade <= 4;

    setHasError(!(isFirstNameValid && isLastNameValid && isGradeValid));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onCreate(firstName, lastName, subjectName, gradeGot);
    setFirstName("");
    setLastName("");
    setSubjectName("");
    setGradeGot("");
  };

  const handleFirstNameChange = (e) => {
    setFirstName(e);
    validateForm({ firstName: e });
  };

  const handleLastNameChange = (e) => {
    setLastName(e);
    validateForm({ lastName: e });
  };

  const handleGradeChange = (e) => {
    const grade = Number(e);
    setGradeGot(e);
    validateForm({ gradeGot: grade });
  };

  return (
    <div className="node-form">
      <form className="grade-form" onSubmit={handleSubmit}>
        <input
          placeholder="First Name"
          value={firstName}
          onChange={(e) => handleFirstNameChange(e.target.value)}
        />
        <input
          placeholder="Last Name"
          value={lastName}
          onChange={(e) => handleLastNameChange(e.target.value)}
        />
        <input
          placeholder="Subject"
          value={subjectName}
          onChange={(e) => setSubjectName(e.target.value)}
        />
        <input
          placeholder="Grade"
          value={gradeGot}
          onChange={(e) => handleGradeChange(e.target.value)}
        />
        <button disabled={hasError} type="submit">
          Submit
        </button>
      </form>
    </div>
  );
}
