import { Link } from "react-router-dom";
import { usePatients } from "../../hooks/usePatients";

const PatientList = () => {
    const { patients, loading, error, deletePatient } = usePatients();

    if (loading) return <p>Loading patients...</p>;
    if (error) return <p>Error: {error}</p>;

    return (
        <div>
            <h2>Liste des Patients</h2>
            <Link to="/add-patient">Ajouter un patient</Link>
            <ul>
                {patients.map((patient) => (
                    <li key={patient.id}>
                        {/* Lien pour voir les détails du patient */}
                        <Link to={`/patient/${patient.id}`} style={{ marginRight: "10px" }}>
                            {patient.firstName} {patient.lastName}
                        </Link>
                        {/* Lien pour modifier le patient */}
                        <Link to={`/edit-patient/${patient.id}`} style={{ marginRight: "10px" }}>
                            Modifier
                        </Link>
                        {/* Bouton pour supprimer le patient */}
                        <button onClick={() => deletePatient(patient.id)}>Supprimer</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default PatientList;
