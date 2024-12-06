import { usePatients } from "../../hooks/usePatients";
import { useParams } from "react-router-dom";

const PatientDetail = () => {
    const { id } = useParams();
    const { patients } = usePatients(); // Utilise les patients depuis le hook

    const patient = patients.find((p) => p.id === parseInt(id));

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
        </div>
    );
};

export default PatientDetail;
