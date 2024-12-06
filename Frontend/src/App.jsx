import { useEffect, useState } from 'react';

const App = () => {
    const [patients, setPatients] = useState([]);
    const [newPatient, setNewPatient] = useState({
        firstName: "",
        lastName: "",
        dateOfBirth: "",
        gender: "",
        address: "",
        phoneNumber: ""
    });
    const [editPatient, setEditPatient] = useState(null);

    useEffect(() => {
        fetchPatients();
    }, []);

    const fetchPatients = async () => {
        try {
            const response = await fetch("https://localhost:7124/api/patient");
            const data = await response.json();
            console.log("Patients récupérés :", data); // Ajoute ce log
            setPatients(data);
        } catch (error) {
            console.error("Error fetching patients:", error);
        }
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setNewPatient({ ...newPatient, [name]: value });
    };

    const handleAddPatient = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch("https://localhost:7124/api/patient", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(newPatient),
            });

            if (response.ok) {
                fetchPatients();
                setNewPatient({
                    firstName: "",
                    lastName: "",
                    dateOfBirth: "",
                    gender: "",
                    address: "",
                    phoneNumber: ""
                });
            } else {
                console.error("Error adding patient");
            }
        } catch (error) {
            console.error("Error:", error);
        }
    };

    const handleDeletePatient = async (id) => {
        try {
            const response = await fetch(`https://localhost:7124/api/patient/${id}`, {
                method: "DELETE",
            });

            if (response.ok) {
                fetchPatients();
            } else {
                console.error("Error deleting patient");
            }
        } catch (error) {
            console.error("Error:", error);
        }
    };

    const handleEditInputChange = (e) => {
        const { name, value } = e.target;
        setEditPatient({ ...editPatient, [name]: value });
    };

    const handleEditPatient = async (e) => {
        e.preventDefault();
        try {
            const response = await fetch(`https://localhost:7124/api/patient/${editPatient.id}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(editPatient),
            });

            if (response.ok) {
                fetchPatients();
                setEditPatient(null);
            } else {
                console.error("Error editing patient");
            }
        } catch (error) {
            console.error("Error:", error);
        }
    };

    return (
        <div>
            <h1>Liste des Patients</h1>
            {patients.length === 0 ? (
                <p>Aucun patient trouvé.</p>
            ) : (
                <ul>
                    {patients.map((patient) => (
                        <li key={patient.id}>
                            {patient.firstName} {patient.lastName} - {patient.address}
                            <button onClick={() => setEditPatient(patient)}>Modifier</button>
                            <button onClick={() => handleDeletePatient(patient.id)}>Supprimer</button>
                        </li>
                    ))}
                </ul>
            )}

            <h2>Ajouter un patient</h2>
            <form onSubmit={handleAddPatient}>
                <input
                    type="text"
                    name="firstName"
                    placeholder="Prénom"
                    value={newPatient.firstName}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    name="lastName"
                    placeholder="Nom"
                    value={newPatient.lastName}
                    onChange={handleInputChange}
                />
                <input
                    type="date"
                    name="dateOfBirth"
                    placeholder="Date de naissance"
                    value={newPatient.dateOfBirth}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    name="gender"
                    placeholder="Genre"
                    value={newPatient.gender}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    name="address"
                    placeholder="Adresse"
                    value={newPatient.address}
                    onChange={handleInputChange}
                />
                <input
                    type="text"
                    name="phoneNumber"
                    placeholder="Téléphone"
                    value={newPatient.phoneNumber}
                    onChange={handleInputChange}
                />
                <button type="submit">Ajouter</button>
            </form>

            {editPatient && (
                <div>
                    <h2>Modifier un patient</h2>
                    <form onSubmit={handleEditPatient}>
                        <input
                            type="text"
                            name="firstName"
                            value={editPatient.firstName}
                            onChange={handleEditInputChange}
                        />
                        <input
                            type="text"
                            name="lastName"
                            value={editPatient.lastName}
                            onChange={handleEditInputChange}
                        />
                        <input
                            type="date"
                            name="dateOfBirth"
                            value={editPatient.dateOfBirth}
                            onChange={handleEditInputChange}
                        />
                        <input
                            type="text"
                            name="gender"
                            value={editPatient.gender}
                            onChange={handleEditInputChange}
                        />
                        <input
                            type="text"
                            name="address"
                            value={editPatient.address}
                            onChange={handleEditInputChange}
                        />
                        <input
                            type="text"
                            name="phoneNumber"
                            value={editPatient.phoneNumber}
                            onChange={handleEditInputChange}
                        />
                        <button type="submit">Mettre à jour</button>
                    </form>
                </div>
            )}
        </div>
    );
};

export default App;
