import React from "react";
import "./GradesLayout.css";
import GradeNode from "../GradeNode/GradeNode";
import Form from "../Form/Form";
import { useState, useEffect } from "react";

export default function GradesLayout() {
  const [grades, setGrades] = useState([]);
  const [error, setError] = useState(null);

  const BASE_URL = "http://localhost:5032/api";

  useEffect(() => {
    const fetchGrades = async () => {
      try {
        const response = await fetch(`${BASE_URL}/Grade/Get`);

        if (!response.ok) {
          throw new Error(
            `HTTP error. Status: ${response.status} ${response.statusText}`
          );
        }

        const data = await response.json();
        setGrades(data);
      } catch (error) {
        setError(error.message + "\nMake sure backend is running");
      }
    };

    fetchGrades();
  }, []);

  const handleDelete = async (id) => {
    try {
      const response = await fetch(`${BASE_URL}/Grade/Delete?gradeId=${id}`, {
        method: "DELETE",
      });

      if (!response.ok) {
        throw new Error(
          `HTTP error. Status: ${response.status} ${response.statusText}`
        );
      }
      console.log("Grade deleted");
      setGrades(grades.filter((grade) => grade.gradeId !== id));
    } catch (error) {
      setError(error.message + "\nCouldn't delete grade");
    }
  };

  const handleUpdate = async (id, newGrade) => {
    try {
      const response = await fetch(
        `${BASE_URL}/Grade/Update?gradeId=${id}&newGrateGot=${newGrade}`,
        {
          method: "PUT",
        }
      );
      if (!response.ok) {
        throw new Error(
          `HTTP error. Status: ${response.status} ${response.statusText}`
        );
      }
      console.log("Grade updated");

      const s = grades.map((grade) =>
        grade.gradeId === id ? { ...grade, gradeGot: newGrade } : grade
      );
      setGrades(s);
    } catch (error) {
      setError(error.message + "\nCouldn't update grade");
    }
  };

  const handleCreate = async (firstName, lastName, subjectName, gradeGot) => {
    try {
      const response = await fetch(
        `${BASE_URL}/Grade/Post?gradeGot=${gradeGot}&firstName=${firstName}&lastName=${lastName}&subjectName=${subjectName}`,
        { method: "POST" }
      );

      if (!response.ok) {
        throw new Error(
          `HTTP error. Status: ${response.status} ${response.statusText}`
        );
      }
      console.log("Grade added");

      const newGrade = await response.json();
      setGrades((prevGrades) => [...prevGrades, newGrade]);
    } catch (error) {
      console.error("Error: ", error.message);
    }
  };

  if (error) {
    alert("Error: " + error);
    setError(null);
  }

  return (
    <section className="main-layout">
      <div>
        <Form onCreate={handleCreate} />
      </div>
      <div></div>
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
