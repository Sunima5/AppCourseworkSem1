// ================= MOOD PIE CHART =================
window.renderMoodPieChart = (labels, data) => {
    const ctx = document.getElementById('moodPieChart');
    if (!ctx) return;

    if (window.moodChart instanceof Chart) {
        window.moodChart.destroy();
    }

    window.moodChart = new Chart(ctx, {
        type: 'pie',
        data: {
            labels: labels,
            datasets: [{
                data: data,
                backgroundColor: [
                    '#22c55e', // Positive
                    '#9ca3af', // Neutral
                    '#ef4444'  // Negative
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { position: 'bottom' }
            }
        }
    });
};

// ================= TAG BAR CHART =================
window.renderTagBarChart = (labels, data) => {
    const ctx = document.getElementById('tagBarChart');
    if (!ctx) return;

    if (window.tagChart instanceof Chart) {
        window.tagChart.destroy();
    }

    window.tagChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Number of Entries',
                data: data,
                backgroundColor: '#3b82f6',
                borderRadius: 6
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    ticks: { stepSize: 1 }
                }
            }
        }
    });
};

// ================= TAG CATEGORY DOUGHNUT =================
window.renderTagCategoryChart = (labels, data) => {
    const canvas = document.getElementById('tagCategoryChart');
    if (!canvas) return;

    if (window.tagCategoryChart instanceof Chart) {
        window.tagCategoryChart.destroy();
    }

    const ctx = canvas.getContext('2d');

    window.tagCategoryChart = new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: labels,
            datasets: [{
                data: data,
                backgroundColor: [
                    '#2563eb', // Work
                    '#9333ea', // Studies
                    '#ec4899', // Relationships
                    '#22c55e', // Health
                    '#14b8a6', // Growth
                    '#f97316', // Lifestyle
                    '#f59e0b', // Travel
                    '#64748b'  // Finance
                ]
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { position: 'bottom' }
            }
        }
    });
};
