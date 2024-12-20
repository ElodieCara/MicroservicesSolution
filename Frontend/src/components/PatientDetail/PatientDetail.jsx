import { useState, useEffect } from "react";
import axios from "axios";
import { usePatients } from "../../hooks/usePatients";
import { useParams } from "react-router-dom";

const PatientDetail = () => {
    const { id } = useParams();
    const { patients } = usePatients(); // Utilise les patients depuis le hook
    const [notes, setNotes] = useState([]);
    const [newNote, setNewNote] = useState("");

    const patient = patients.find((p) => p.id === parseInt(id));

     useEffect(() => {
        if (patient) {
            axios.get(`https://localhost:44336/api/Notes/patient/${patient.id}`)
                .then((response) => {
                    console.log("Notes récupérées :", response.data); // Ajoutez ce log
                    setNotes(response.data)
                })
                .catch((error) => console.error("Erreur lors du chargement des notes:", error));
        }
    }, [patient]);

    const addNote = () => {
        const note = {
            PatId: patient.id,
            Patient: `${patient.firstName} ${patient.lastName}`,
            NoteContent: newNote,
        };

        axios.post("https://localhost:44336/api/Notes", note)
            .then((response) => {
                setNotes([...notes, response.data]);
                setNewNote("");
            })
            .catch((error) => console.error("Erreur lors de l'ajout de la note:", error));
    };

    console.log("Patient :", patient);
    console.log("Notes :", notes);

    if (!patient) {
        return <p>Patient not found.</p>;
    }

    return (
        <div>
            <h1>Patient Details</h1>
            <p><strong>First Name:</strong> {patient.firstName}</p>
            <p><strong>Last Name:</strong> {patient.lastName}</p>
            <p><strong>Date of Birth:</strong> {new Date(patient.dateOfBirth).toLocaleDateString()}</p>
            <p><strong>Gender:</strong> {patient.gender}</p>
            <p><strong>Address:</strong> {patient.address}</p>
            <p><strong>Phone Number:</strong> {patient.phoneNumber}</p>

            {/* Section Notes */}
            <div>
                <h2>Notes</h2>
                <ul>
                    {notes.map((note) => (
                        <li key={note.id}>
                            <strong>{note.noteContent}</strong>
                        </li>
                    ))}
                </ul>
                <textarea
                    value={newNote}
                    onChange={(e) => setNewNote(e.target.value)}
                    placeholder="Ajouter une note..."
                    rows="4"
                    cols="50"
                />
                <button onClick={addNote}>Ajouter une note</button>
            </div>
        </div>
    );
};

export default PatientDetail;
