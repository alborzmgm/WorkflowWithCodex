import { evaluateVisibleWhen } from './visibleWhen.js';

const formSchema = [
  {
    id: 'favoriteFruits',
    type: 'checkboxList',
    label: 'Pick your favorite fruits',
    options: [
      { label: 'Apple', value: 'apple' },
      { label: 'Banana', value: 'banana' },
      { label: 'Orange', value: 'orange' },
      { label: 'Other', value: 'other' }
    ]
  },
  {
    id: 'customFruit',
    type: 'text',
    label: 'What other fruit do you like?',
    visibleWhen: { field: 'favoriteFruits', operator: 'contains', value: 'other' }
  },
  {
    id: 'fruitReason',
    type: 'text',
    label: 'Why do you like these fruits?',
    visibleWhen: { field: 'favoriteFruits', operator: 'hasValue' }
  },
  {
    id: 'noneSelectedMessage',
    type: 'info',
    label: 'No fruits selected yet.',
    visibleWhen: { field: 'favoriteFruits', operator: 'isEmpty' }
  },
  {
    id: 'differentCombinationNotice',
    type: 'info',
    label: 'You selected a fruit combination different from [apple, banana].',
    visibleWhen: {
      field: 'favoriteFruits',
      operator: 'notEquals',
      value: ['apple', 'banana']
    }
  }
];

const form = document.querySelector('#dynamic-form');
const values = Object.fromEntries(formSchema.map((field) => [field.id, field.type === 'checkboxList' ? [] : '']));

const escapeHtml = (value) =>
  String(value)
    .replaceAll('&', '&amp;')
    .replaceAll('<', '&lt;')
    .replaceAll('>', '&gt;')
    .replaceAll('"', '&quot;')
    .replaceAll("'", '&#39;');

const renderCheckboxList = (field) => {
  const selected = values[field.id] ?? [];
  const chips = selected.length
    ? `<div class="selection-chips">${selected
        .map((item) => `<span class="chip">${escapeHtml(item)}</span>`)
        .join('')}</div>`
    : '<p class="selection-placeholder">No options selected</p>';

  return `
    <fieldset class="field checkbox-group">
      <legend>${escapeHtml(field.label)}</legend>
      <div class="checkbox-grid">
        ${field.options
          .map(
            (option) => `
              <label class="checkbox-option ${selected.includes(option.value) ? 'selected' : ''}">
                <input type="checkbox" name="${field.id}" value="${option.value}" ${
                  selected.includes(option.value) ? 'checked' : ''
                } />
                <span>${escapeHtml(option.label)}</span>
              </label>`
          )
          .join('')}
      </div>
      ${chips}
    </fieldset>
  `;
};

const renderText = (field) => `
  <label class="field text-field">
    <span>${escapeHtml(field.label)}</span>
    <input type="text" name="${field.id}" value="${escapeHtml(values[field.id] ?? '')}" />
  </label>
`;

const renderInfo = (field) => `
  <p class="field info">${escapeHtml(field.label)}</p>
`;

const renderField = (field) => {
  if (!evaluateVisibleWhen(field.visibleWhen, values)) {
    return '';
  }

  if (field.type === 'checkboxList') {
    return renderCheckboxList(field);
  }

  if (field.type === 'text') {
    return renderText(field);
  }

  return renderInfo(field);
};

const renderForm = () => {
  form.innerHTML = formSchema.map(renderField).join('');
};

form.addEventListener('change', (event) => {
  const target = event.target;

  if (!(target instanceof HTMLInputElement)) {
    return;
  }

  if (target.type === 'checkbox') {
    const selectedValues = Array.from(form.querySelectorAll(`input[name="${target.name}"]:checked`)).map(
      (input) => input.value
    );
    values[target.name] = selectedValues;
  } else {
    values[target.name] = target.value;
  }

  renderForm();
});

form.addEventListener('input', (event) => {
  const target = event.target;

  if (!(target instanceof HTMLInputElement) || target.type !== 'text') {
    return;
  }

  values[target.name] = target.value;
});

renderForm();
