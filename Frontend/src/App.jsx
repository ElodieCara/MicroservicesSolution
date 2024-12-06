import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import PatientList from "./components/PatientList/PatientList";
import AddPatientForm from "./components/AddPatientForm/AddPatientForm";
import EditPatientForm from "./components/EditPatientForm/EditPatientForm";
import PatientDetail from "./components/PatientDetail/PatientDetail";

const App = () => {
    return (
        <Router>
            <div>
                <h1>Patient Management System</h1>
                <Routes>
                    {/* Route pour afficher la liste des patients */}
                    <Route path="/" element={<PatientList />} />

                    {/* Route pour ajouter un patient */}
                    <Route path="/add-patient" element={<AddPatientForm />} />

                    {/* Route pour modifier un patient */}
                    <Route path="/edit-patient/:id" element={<EditPatientForm />} />

                    <Route path="/patient/:id" element={<PatientDetail />} />

                </Routes>
            </div>
        </Router>
    );
};

export default App;
