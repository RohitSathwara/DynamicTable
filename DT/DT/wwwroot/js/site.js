document.addEventListener("DOMContentLoaded", function () {
    document.getElementById('workingDays').addEventListener('input', updateTotalHours);
    document.getElementById('subjectsPerDay').addEventListener('input', updateTotalHours);
    document.getElementById('timeTableForm').addEventListener('submit', validateForm);
});

function updateTotalHours() {
    let workingDays = parseInt(document.getElementById('workingDays').value) || 0;
    let subjectsPerDay = parseInt(document.getElementById('subjectsPerDay').value) || 0;
    let totalHours = workingDays * subjectsPerDay;
    document.getElementById('totalHours').value = totalHours > 0 ? totalHours : '';
}

function generateSubjectInputs() {
    let workingDays = parseInt(document.getElementById('workingDays').value);
    let subjectsPerDay = parseInt(document.getElementById('subjectsPerDay').value);
    let totalHours = workingDays * subjectsPerDay;

    if (workingDays < 1 || workingDays > 7 || subjectsPerDay < 1 || subjectsPerDay > 9) {
        alert("Please enter valid Working Days (1-7) and Subjects Per Day (1-9).");
        return;
    }

    document.getElementById('totalHours').value = totalHours;
    let container = document.getElementById('subjectsContainer');
    container.innerHTML = '';

    for (let i = 0; i < 4; i++) {
        let div = document.createElement('div');
        div.classList.add("mb-2");
        div.innerHTML = `
                <div class="input-group">
                    <input type="text" name="Subjects[${i}].Name" class="form-control" placeholder="Subject Name" required />
                    <input type="number" name="Subjects[${i}].Hours" class="form-control subjectHours" placeholder="Hours" min="1" required oninput="validateTotalHours()" />
                </div>
            `;
        container.appendChild(div);
    }

    document.getElementById('submitButton').disabled = true;
}

function validateTotalHours() {
    let totalHours = parseInt(document.getElementById('totalHours').value);
    let subjectHourInputs = document.querySelectorAll('.subjectHours');
    let enteredTotal = Array.from(subjectHourInputs).reduce((sum, input) => sum + (parseInt(input.value) || 0), 0);

    let validationMessage = document.getElementById('hoursValidationMessage');
    if (enteredTotal < totalHours) {
        validationMessage.innerText = `Total entered hours (${enteredTotal}) is less than required (${totalHours}).`;
        document.getElementById('submitButton').disabled = true;
    } else if (enteredTotal > totalHours) {
        validationMessage.innerText = `Total entered hours (${enteredTotal}) exceeds required (${totalHours}).`;
        document.getElementById('submitButton').disabled = true;
    } else {
        validationMessage.innerText = "";
        document.getElementById('submitButton').disabled = false;
    }
}

function validateForm(event) {
    validateTotalHours();
    if (document.getElementById('submitButton').disabled) {
        event.preventDefault();
    }
}