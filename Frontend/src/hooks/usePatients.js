import { useState, useEffect } from "react";

export const usePatients = () => {
    const [patients, setPatients] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const apiUrl = "https://localhost:7124/api/patient"; // URL de l'API Gateway

    // Récupérer tous les patients
    const fetchPatients = async () => {
        setLoading(true);
        setError(null);

        try {
            const response = await fetch(apiUrl);
            if (!response.ok) {
                throw new Error("Failed to fetch patients");
            }
            const data = await response.json();
            setPatients(data);
        } catch (err) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    // Ajouter un patient
    const addPatient = async (patient) => {
        try {
            const response = await fetch(apiUrl, {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(patient),
            });

            if (response.ok) {
                fetchPatients(); // Rafraîchit la liste après ajout
            } else {
                throw new Error("Failed to add patient");
            }
        } catch (err) {
            setError(err.message);
        }
    };

    // Mettre à jour un patient
    const updatePatient = async (id, updatedPatient) => {
        try {
            const response = await fetch(`${apiUrl}/${id}`, {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(updatedPatient),
            });

            if (response.ok) {
                fetchPatients(); // Rafraîchit la liste après mise à jour
            } else {
                throw new Error("Failed to update patient");
            }
        } catch (err) {
            setError(err.message);
        }
    };

    // Supprimer un patient
    const deletePatient = async (id) => {
        try {
            const response = await fetch(`${apiUrl}/${id}`, {
                method: "DELETE",
            });

            if (response.ok) {
                setPatients((prev) => prev.filter((patient) => patient.id !== id));
            } else {
                throw new Error("Failed to delete patient");
            }
        } catch (err) {
            setError(err.message);
        }
    };

    useEffect(() => {
        fetchPatients();
    }, []);

    return {
        patients,
        loading,
        error,
        fetchPatients,
        addPatient,
        updatePatient,
        deletePatient,
    };
};
