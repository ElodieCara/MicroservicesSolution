import { useState, useEffect, useCallback } from "react";

export const usePatients = () => {
    const [patients, setPatients] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const apiUrl = "http://localhost:5100/api/patients"; // URL de l'API Gateway

    const fetchPatients = useCallback(async () => {
        const token = localStorage.getItem("jwt"); // R�cup�rer le token � chaque appel

        setLoading(true);
        setError(null);

        try {
            const response = await fetch(apiUrl, {
                headers: {
                    Authorization: `Bearer ${token}`, // Inclure le token
                },
            });

            console.log("Response status:", response.status);

            if (!response.ok) {
                throw new Error(`Failed to fetch patients: ${response.statusText}`);
            }

            const data = await response.json();
            console.log("Patients fetched:", data);
            setPatients(data);
        } catch (err) {
            console.error("Error fetching patients:", err.message);
            setError(err.message);
        } finally {
            setLoading(false);
        }
    }, [apiUrl]);

    useEffect(() => {
        fetchPatients();
    }, [fetchPatients]);

    const addPatient = useCallback(
        async (patient) => {
            const token = localStorage.getItem("jwt"); // R�cup�rer le token � chaque appel
            if (!token) {
                setError("Missing token. Please log in.");
                return;
            }

            try {
                const response = await fetch(apiUrl, {
                    method: "POST",
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${token}`, // Inclure le token
                    },
                    body: JSON.stringify(patient),
                });

                if (response.ok) {
                    fetchPatients(); // Rafra�chit la liste apr�s ajout
                } else {
                    throw new Error(`Failed to add patient: ${response.statusText}`);
                }
            } catch (err) {
                setError(err.message);
            }
        },
        [apiUrl, fetchPatients]
    );

    const updatePatient = useCallback(
        async (id, updatedPatient) => {
            const token = localStorage.getItem("jwt");
            if (!token) {
                setError("Missing token. Please log in.");
                return;
            }

            try {
                const response = await fetch(`${apiUrl}/${id}`, {
                    method: "PUT",
                    headers: {
                        "Content-Type": "application/json",
                        Authorization: `Bearer ${token}`, // Inclure le token
                    },
                    body: JSON.stringify(updatedPatient),
                });

                if (response.ok) {
                    fetchPatients(); // Rafra�chit la liste apr�s mise � jour
                } else {
                    throw new Error(`Failed to update patient: ${response.statusText}`);
                }
            } catch (err) {
                setError(err.message);
            }
        },
        [apiUrl, fetchPatients]
    );

    const deletePatient = useCallback(
        async (id) => {
            const token = localStorage.getItem("jwt");
            if (!token) {
                setError("Missing token. Please log in.");
                return;
            }

            try {
                const response = await fetch(`${apiUrl}/${id}`, {
                    method: "DELETE",
                    headers: {
                        Authorization: `Bearer ${token}`, // Inclure le token
                    },
                });

                if (response.ok) {
                    setPatients((prev) => prev.filter((patient) => patient.id !== id));
                } else {
                    throw new Error(`Failed to delete patient: ${response.statusText}`);
                }
            } catch (err) {
                setError(err.message);
            }
        },
        [apiUrl]
    );

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
