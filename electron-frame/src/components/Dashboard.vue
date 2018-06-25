<template>
<v-content>
      <v-container fluid fill-height>
        <v-layout flex justify-center>
          <v-flex xs6 sm6 align-content-center>
            <v-card color="white" class="black--text" d-flex>
              <v-card-title primary-title>
                <div style="width:100%;">
                  <h3 class="headline mb-0">Welcome</h3>
                  <v-data-table
                    :headers="headers"
                    :items="users"
                    hide-actions
                    class="elevation-1">
                    <template slot="items" slot-scope="props">
                    <td>{{ props.item.name }}</td>
                    <td class="text-xs-left">{{ props.item.dob }}</td>
                    <td class="text-xs-left">{{ props.item.gender }}</td>
                    <td class="text-xs-left">{{ props.item.nhsNumber }}</td>
                    </template>
                </v-data-table>
                </div>
              </v-card-title>
              <v-card-actions>
              </v-card-actions>
            </v-card>
          </v-flex>
        </v-layout>
      </v-container>
    </v-content>
</template>

<script>
export default {
  name: "dashboard",
  components: {},
  data() {
    return {
      // Some mock data to fill the page
        headers: [
          {
            text: 'Recent Patients',
            align: 'left',
            sortable: false,
            value: 'name'
          },
          { text: 'DOB', value: 'dob' },
          { text: 'Gender', value: 'gender' },
          { text: 'NHS Number', value: 'nhsNumber' },
        ],
        users: [
          {
            value: false,
            name: 'Aiko Haney',
            dob: '29-Aug-1954',
            gender: 'Female',
            nhsNumber: '999 999 9024',
          },
          {
            value: false,
            name: 'Blair Wells',
            dob: '03-Mar-1969',
            gender: 'Female',
            nhsNumber: '999 999 9007',
          },
          {
            value: false,
            name: 'Clio Cooke',
            dob: '10-Apr-1964',
            gender: 'Female',
            nhsNumber: '999 999 9087',
          },
          {
            value: false,
            name: 'Bryar Hendricks',
            dob: '17-Sep-1976',
            gender: 'Female',
            nhsNumber: '999 999 9038',
          },
          {
            value: false,
            name: 'Carol Griffith',
            dob: '03-Aug-1927',
            gender: 'Male',
            nhsNumber: '999 999 9049',
          },
          {
            value: false,
            name: 'Alexis Greer',
            dob: '23-Apr-1941',
            gender: 'Female',
            nhsNumber: '999 999 9097',
          }
          ]
    };
  }
};
</script>
