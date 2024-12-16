import React from 'react';
import "./GradesLayout.css";
import GradeNode from "../GradeNode/GradeNode";
import Form from "../Form/Form";
import { useState, useEffect } from "react";

export default function GradesLayout() {
  const [grades, setGrades] = useState([]);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchGrades = async () => {
      try {
        const response = await fetch(`http://localhost:5032/api/Grade/Get`);
        if (!response.ok) {
          throw new Error("HTTP error. Status: ");
        }
        const data = await response.json();
        setGrades(data);
      } catch (err) {
        setError(err.message);
      }
    };

    fetchGrades();
  }, []);

  const handleDelete = async (id) => {
    try {
      const response = await fetch(
        `http://localhost:5032/api/Grade/Delete?gradeId=${id}`,
        {
          method: "DELETE",
        }
      );
      if (response.ok) {
        console.log("Grade deleted");
        setGrades(grades.filter((grade) => grade.gradeId !== id));
      } else {
        console.error("Delete error");
      }
    } catch (error) {
      console.error("Error: ", error.message);
    }
  };

  const handleUpdate = async (id, newGrade) => {
    try {
      const response = await fetch(
        `http://localhost:5032/api/Grade/Update?gradeId=${id}&newGrateGot=${newGrade}`,
        {
          method: "PUT",
        }
      );
      if (response.ok) {
        console.log("Grade updated");

        const s = grades.map((grade) =>
          grade.gradeId === id ? { ...grade, gradeGot: newGrade } : grade
        );
        setGrades(s);
      } else {
        console.error("Failed to update: ", error.message);
      }
    } catch (error) {
      console.error("Error: ", error.message);
    }
  };

  const handleCreate = async (firstName, lastName, subjectName, gradeGot) => {
    try {
      const response = await fetch(
        `http://localhost:5032/api/Grade/Post?gradeGot=${gradeGot}&firstName=${firstName}&lastName=${lastName}&subjectName=${subjectName}`,
        { method: "POST" }
      );

      if (response.ok) {
        console.log("Grade added");

        const newGrade = await response.json();
        setGrades((prevGrades) => [...prevGrades, newGrade]);
      } else {
        console.error("Failed to add Grade: ", error.message);
      }
    } catch (error) {
      console.error("Error: ", error.message);
    }
  };

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <section className="main-layout">
      <div>
        <Form onCreate={handleCreate} />
      </div>
      <div>
      </div>
      <div className="list-layout">
        {grades.map((grade) => (
          <GradeNode
            gradeId={grade.gradeId}
            firstName={grade.student.firstName}
            lastName={grade.student.lastName}
            subjectName={grade.subject.subjectName}
            gradeGot={grade.gradeGot}
            dateTime={grade.dateTime.slice(0, 10)}
            onUpdate={handleUpdate}
            onDelete={handleDelete}
          />
        ))}
      </div>
    </section>
  );
}
